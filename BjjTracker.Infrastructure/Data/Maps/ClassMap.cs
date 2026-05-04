using BjjTracker.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BjjTracker.Infrastructure.Data.Maps;

public class ClassMap : IEntityTypeConfiguration<Class>
{
	public void Configure(EntityTypeBuilder<Class> builder)
	{
		builder.ToTable("Classes");
		builder.HasKey(e => e.Id);

		builder.Property(e => e.CreatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");
		builder.Property(e => e.UpdatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");
		
		builder.Property(e => e.SchoolId).IsRequired();
		builder.Property(e => e.TeacherId).IsRequired();
		
		builder.HasOne(e => e.School)
			.WithMany(s => s.Classes)
			.HasForeignKey(e => e.SchoolId);

		builder.HasOne(e => e.Teacher)
			.WithMany()
			.HasForeignKey(e => e.TeacherId);
	}
}