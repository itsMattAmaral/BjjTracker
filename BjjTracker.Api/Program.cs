using BjjTracker.Api.Extensions;
using BjjTracker.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services
	.AddApplicationConfiguration(builder.Configuration)
	.AddRepositories()
	.AddMediatRServices()
	.AddAuthenticationServices(builder.Configuration)
	.AddAuthorization(builder.Configuration)
	.AddSwaggerServices();
var app = builder.Build();
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<BjjTrackerDbContext>();
    db.Database.Migrate();

}
app = app.ConfigurePipeline(app.Environment);
app.Run();