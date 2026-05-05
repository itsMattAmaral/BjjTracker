using BjjTracker.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BjjTracker.Infrastructure.Data.Maps;

public class TeacherMap : IEntityTypeConfiguration<Teacher>
{
	public void Configure(EntityTypeBuilder<Teacher> builder)
	{
		builder.Property(t => t.IsSchoolOwner).HasDefaultValue(false);
		builder.HasOne(t => t.School).WithMany(s => s.Teachers).HasForeignKey(t => t.SchoolId);
		builder.HasOne(t => t.SchoolOwned).WithMany(s => s.Owners).HasForeignKey(t => t.SchoolOwnedId).OnDelete(DeleteBehavior.SetNull);
	}
}