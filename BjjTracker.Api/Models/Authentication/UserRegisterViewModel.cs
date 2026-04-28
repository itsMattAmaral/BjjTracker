using System.ComponentModel.DataAnnotations;
using BjjTracker.Application.User.Commands.Actions;
using BjjTracker.Domain.Enums;

namespace BjjTracker.Api.Models.Authentication;

public class UserRegisterViewModel
{
	[Required]
	[EmailAddress]
	public required string Email { get; set; }
	
	[Required]
	[MinLength(6, ErrorMessage = "Password must be at least 6 characters long.")]
	public required string Password { get; set; }

	[Required]
	[EnumDataType(typeof(Roles), ErrorMessage = "Invalid role value.")]
	public int Role { get; set; } = 1;

	public RegisterUserCommand GetCommand()
	{
		return new RegisterUserCommand(Email, Password, Role);
	}
}