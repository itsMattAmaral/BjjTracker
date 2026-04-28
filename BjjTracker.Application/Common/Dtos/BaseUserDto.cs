using BjjTracker.Domain.Enums;

namespace BjjTracker.Application.Common.Dtos;

public class BaseUserDto
{
	public int Id { get; set; }
	public string? FirstName { get; set; }
	public string? LastName { get; set; }
	public string? Email { get; set; }
	public Roles Role { get; set; }
	public DateTime CreatedAt { get; set; }
	public DateTime? UpdatedAt { get; set; }
}