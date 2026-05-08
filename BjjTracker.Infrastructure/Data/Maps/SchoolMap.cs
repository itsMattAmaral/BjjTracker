using BjjTracker.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BjjTracker.Infrastructure.Data.Maps;

public class SchoolMap : IEntityTypeConfiguration<School>
{
	public void Configure(EntityTypeBuilder<School> builder)
	{
		builder.ToTable("Schools");
		builder.HasKey(e => e.Id);
		builder.Property(e => e.Name).HasMaxLength(100);
		builder.Property(e => e.Document).IsRequired().HasMaxLength(50);
		builder.HasIndex(e => e.Document).IsUnique();
		builder.Property(e => e.ContactEmail).IsRequired().HasMaxLength(200);
		builder.Property(e => e.ContactPhone).HasMaxLength(20);
		builder.Property(e => e.CreatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");
		builder.Property(e => e.UpdatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");
	}
}