using System.ComponentModel.DataAnnotations;
using BjjTracker.Application.User.Commands.Actions;

namespace BjjTracker.Api.Models.Authentication;

public class UserLoginViewModel
{
	[Required]
	[EmailAddress]
	public string Email { get; set; }
	[Required]
	public string Password { get; set; }
	
	public LoginUserCommand GetCommand()
	{
		return new LoginUserCommand(Email, Password);
	}
}