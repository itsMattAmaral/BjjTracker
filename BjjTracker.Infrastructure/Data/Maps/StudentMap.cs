using BjjTracker.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BjjTracker.Infrastructure.Data.Maps;

public class StudentMap : IEntityTypeConfiguration<Student>
{
	public void Configure(EntityTypeBuilder<Student> builder)
	{
		builder.ToTable("Users");
		builder.HasOne(s => s.School).WithMany(s => s.Students).HasForeignKey(s => s.SchoolId);
	}
}