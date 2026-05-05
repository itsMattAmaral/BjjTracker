using BjjTracker.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BjjTracker.Infrastructure.Data.Maps;

public class AttendanceRequestMap : IEntityTypeConfiguration<AttendanceRequest>
{
	public void Configure(EntityTypeBuilder<AttendanceRequest> builder)
	{
		builder.ToTable("AttendanceRequests");
		builder.Property(e => e.CreatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");
		builder.Property(e => e.UpdatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");
		builder.Property(e => e.StudentId).IsRequired();
		builder.Property(e => e.ClassId).IsRequired();
		builder.HasKey(e => new { e.ClassId, e.StudentId });
		builder.HasOne(e => e.Class)
			.WithMany(c => c.AttendanceRequests)
			.HasForeignKey(e => e.ClassId);
		builder.HasOne(e => e.Student)
			.WithMany()
			.HasForeignKey(e => e.StudentId);
	}
}