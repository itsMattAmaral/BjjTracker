using BjjTracker.Domain.Entities;
using BjjTracker.Infrastructure.Data.Maps;
using Microsoft.EntityFrameworkCore;

namespace BjjTracker.Infrastructure.Data;

public class BjjTrackerDbContext(DbContextOptions<BjjTrackerDbContext> options): DbContext(options)
{
	public DbSet<User> Users => Set<User>();
	public DbSet<Class> Classes => Set<Class>();
	public DbSet<School> Schools => Set<School>();
	public  DbSet<AttendanceRequest> AttendanceRequests => Set<AttendanceRequest>();

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.ApplyConfiguration(new UserMap());
		modelBuilder.ApplyConfiguration(new TeacherMap());
		modelBuilder.ApplyConfiguration(new StudentMap());
		modelBuilder.ApplyConfiguration(new ClassMap());
		modelBuilder.ApplyConfiguration(new SchoolMap());
		modelBuilder.ApplyConfiguration(new AttendanceRequestMap());
		
		
		base.OnModelCreating(modelBuilder);
	}
}