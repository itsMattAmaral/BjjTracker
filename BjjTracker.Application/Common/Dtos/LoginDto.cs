namespace BjjTracker.Application.Common.Dtos;

public class LoginDto
{
	public required string Token { get; set; }
	public required string Role { get; set; }
}