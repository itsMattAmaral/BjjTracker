using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.Text;
using BjjTracker.Application.Authentication;
using BjjTracker.Domain.Interfaces;
using BjjTracker.Infrastructure.Data;
using BjjTracker.Infrastructure.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace BjjTracker.Api.Extensions;

public static class ServiceCollectionExtensions
{
	public static IServiceCollection AddApplicationConfiguration(this IServiceCollection services,
		IConfiguration configuration)
	{
		var connectionString = configuration.GetConnectionString("DefaultConnection");
		if (string.IsNullOrEmpty(connectionString)) throw new InvalidOperationException("Connection string 'DefaultConnection' is not configured.");

		services.AddDbContext<BjjTrackerDbContext>(options => options.UseNpgsql(connectionString, optionsBuilder => optionsBuilder.MigrationsAssembly("BjjTracker.Infrastructure").MigrationsHistoryTable("__EFMigrationsHistory", "public")));
		services.AddControllers();
		services.AddEndpointsApiExplorer();
		
		return services;
	}

	public static IServiceCollection AddRepositories(this IServiceCollection services)
	{
		services.AddScoped<IUserRepository, UserRepository>();
		services.AddScoped<ITeacherRepository, TeacherRepository>();
		services.AddScoped<IStudentRepository, StudentRepository>();
		services.AddScoped<ISchoolRepository, SchoolRepository>();
		services.AddScoped<IClassRepository, ClassRepository>();
		return services;
	}

	public static IServiceCollection AddMediatRServices(this IServiceCollection services)
	{
		var assemblyTypes = new[]
		{
			typeof(Application.User.Commands.UserCommandHandler),
			typeof(Application.Teacher.Commands.TeacherCommandHandler),
			typeof(Application.Student.Commands.StudentCommandHandler),
			typeof(Application.Student.Queries.StudentQueryHandler),
			typeof(Application.Teacher.Queries.TeacherQueryHandler),
			typeof(Application.School.Commands.SchoolCommandHandler),
			typeof(Application.Class.Commands.ClassCommandHandler),
			typeof(Application.Class.Queries.ClassQueryHandler)
		};
		
		foreach (var type in assemblyTypes)
		{
			services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining(type));
		}
		
		return services;
	}
	
	public static IServiceCollection AddAuthenticationServices(this IServiceCollection services, IConfiguration configuration)
	{
		services.AddAuthentication(config =>
		{
			config.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
			config.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
			config.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
		}).AddJwtBearer(options =>
		{
			options.RequireHttpsMetadata = false;
			options.SaveToken = true;
			options.TokenValidationParameters = new TokenValidationParameters
			{
				ValidateIssuerSigningKey = true,
				IssuerSigningKey = new SymmetricSecurityKey(
					Encoding.UTF8.GetBytes(
						configuration["ApplicationSettings:JWT_Secret"] ?? string.Empty)
					),
				ValidateIssuer = false,
				ValidateAudience = false,
				ClockSkew = TimeSpan.Zero,
				RoleClaimType = ClaimTypes.Role
			};
		});
		services.AddScoped<IJwtAuthenticationHelper, JwtAuthenticationHelper>();
		return services;
	}

	public static IServiceCollection AddAuthorization(this IServiceCollection services, IConfiguration configuration)
	{
		services.AddAuthorizationBuilder()
			.AddPolicy("TeacherOnly", policy => policy.RequireRole("Teacher"))
			.AddPolicy("StudentOnly", policy => policy.RequireRole("Student"))
			.AddPolicy("AnyUser", policy => policy.RequireRole("Student", "Teacher"))
			.AddPolicy("SchoolOwner", policy => policy.RequireRole("Teacher").RequireClaim("IsSchoolOwner", "True"));
		return services;
	}

	public static IServiceCollection AddSwaggerServices(this IServiceCollection services)
	{
		services.AddSwaggerGen(options =>
		{
			options.SwaggerDoc("v1", new Microsoft.OpenApi.OpenApiInfo
			{
				Version = "v1",
				Title = "BjjTracker API",
				Description = "API for managing Brazilian Jiu-Jitsu schools, teachers, and students."
			});
			
			var jwtSecurityScheme = new Microsoft.OpenApi.OpenApiSecurityScheme
			{
				Scheme = "bearer",
				BearerFormat = "JWT",
				Name = "JWT Authentication",
				In = Microsoft.OpenApi.ParameterLocation.Header,
				Type = Microsoft.OpenApi.SecuritySchemeType.Http,
				Description = "Enter JWT Bearer token **_only_**"
			};
			
			options.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, jwtSecurityScheme);
			options.AddSecurityRequirement(document => new Microsoft.OpenApi.OpenApiSecurityRequirement
			{
				{
					new Microsoft.OpenApi.OpenApiSecuritySchemeReference(JwtBearerDefaults.AuthenticationScheme,
						document),
					[]
				}
			});
		});
		return services;
	}
}