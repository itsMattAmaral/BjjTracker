using BjjTracker.Domain.Entities;
using BjjTracker.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace BjjTracker.Infrastructure.Data;

public class BjjTrackerDbContext(DbContextOptions<BjjTrackerDbContext> options): DbContext(options)
{
	public DbSet<User> Users => Set<User>();
	public DbSet<Class> Classes => Set<Class>();
	public DbSet<School> Schools => Set<School>();

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.Entity<User>()
			.HasDiscriminator<Roles>("Role")
			.HasValue<Student>(Roles.Student)
			.HasValue<Teacher>(Roles.Teacher);
		modelBuilder.Entity<User>(entity =>
		{
			entity.HasKey(e => e.Id);
			entity.Property(e => e.Email).IsRequired().HasMaxLength(200);
			entity.Property(e => e.Password).IsRequired().HasMaxLength(200);
			entity.Property(e => e.CreatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");
			entity.Property(e => e.UpdatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");
		});
		
		modelBuilder.Entity<Teacher>(entity =>
		{
			entity.HasOne(t => t.School)
				.WithMany(s => s.Teachers)
				.HasForeignKey(t => t.SchoolId);
		});

		modelBuilder.Entity<Student>(entity =>
		{
			entity.HasOne(s => s.School)
				.WithMany(sc => sc.Students)
				.HasForeignKey(s => s.SchoolId);
		});

		modelBuilder.Entity<Class>(entity =>
		{
			entity.HasKey(e => e.Id);
			entity.Property(e => e.CreatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");
			entity.Property(e => e.UpdatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");
			entity.Property(e => e.SchoolId).IsRequired();
			entity.Property(e => e.TeacherId).IsRequired();
			entity.HasOne(e => e.School)
				.WithMany(s => s.Classes)
				.HasForeignKey(e => e.SchoolId);

			entity.HasOne(e => e.Teacher)
				.WithMany()
				.HasForeignKey(e => e.TeacherId);

		});

		modelBuilder.Entity<School>(entity =>
		{
			entity.HasKey(e => e.Id);
			entity.Property(e => e.Name).HasMaxLength(100);
			entity.Property(e => e.Document).IsRequired().HasMaxLength(50);
			entity.HasIndex(e => e.Document).IsUnique();
			entity.Property(e => e.ContactEmail).IsRequired().HasMaxLength(200);
			entity.Property(e => e.ContactPhone).HasMaxLength(20);
			entity.Property(e => e.CreatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");
			entity.Property(e => e.UpdatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");
		});
		
		modelBuilder.Entity<AttendanceRequest>(entity =>
        {
            entity.HasKey(e => new { e.ClassId, e.StudentId });
            entity.HasOne(e => e.Class)
	            .WithMany(c => c.AttendanceRequests)
	            .HasForeignKey(e => e.ClassId);
			entity.HasOne(e => e.Student)
	            .WithMany()
	            .HasForeignKey(e => e.StudentId);
        });
		
		
		base.OnModelCreating(modelBuilder);
	}
}