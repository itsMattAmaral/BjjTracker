using BjjTracker.Domain.Entities;
using BjjTracker.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BjjTracker.Infrastructure.Data.Maps;

public class UserMap : IEntityTypeConfiguration<User>
{
	public void Configure(EntityTypeBuilder<User> builder)
	{
		builder.ToTable("Users");
		builder.HasDiscriminator<Roles>("Role")
			.HasValue<Student>(Roles.Student)
			.HasValue<Teacher>(Roles.Teacher);
		builder.HasKey(e => e.Id);
		builder.Property(e => e.Email).IsRequired().HasMaxLength(200);
		builder.Property(e => e.Password).IsRequired().HasMaxLength(200);
		builder.Property(e => e.CreatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");
		builder.Property(e => e.UpdatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");
	}
}