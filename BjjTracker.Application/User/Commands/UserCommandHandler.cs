using System.Security.Authentication;
using BjjTracker.Application.Authentication;
using BjjTracker.Application.Common.Dtos;
using BjjTracker.Application.User.Commands.Actions;
using BjjTracker.Domain.Enums;
using BjjTracker.Domain.Exceptions.User;
using BjjTracker.Domain.Helpers;
using BjjTracker.Domain.Interfaces;
using MediatR;

namespace BjjTracker.Application.User.Commands;

public class UserCommandHandler(IUserRepository userRepository, IJwtAuthenticationHelper jwtAuthenticationHelper) :
	IRequestHandler<RegisterUserCommand>,
	IRequestHandler<LoginUserCommand, LoginDto>
{
	private readonly IUserRepository _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
	private readonly IJwtAuthenticationHelper _jwtAuthenticationHelper = jwtAuthenticationHelper ?? throw new ArgumentNullException(nameof(jwtAuthenticationHelper));
	
	public async Task Handle(RegisterUserCommand request, CancellationToken cancellationToken)
	{
		if (await _userRepository.ExistsAsync(request.Email, cancellationToken))
		{
			throw new ThisEmailAlreadyExistsException();
		}
		
		var password =  PasswordHandler.HashPassword(request.Password);

		switch ((Roles)request.Role)
		{
			case Roles.Student:
			{
				var newStudent = new Domain.Entities.Student
				{
					Email = request.Email,
					Password = password,
					Role = (Roles)request.Role,
					CreatedAt = DateTime.UtcNow
				};
				await _userRepository.AddAsync(newStudent, cancellationToken);
				break;
			}
			case Roles.Teacher:
			{
				var newTeacher = new Domain.Entities.Teacher
				{
					Email = request.Email,
					Password = password,
					Role = Roles.Teacher,
					CreatedAt = DateTime.UtcNow
				};
				await _userRepository.AddAsync(newTeacher, cancellationToken);
				break;
			}
			default:
				throw new RoleNotFoundException();
		}

	}

	public async Task<LoginDto> Handle(LoginUserCommand request, CancellationToken cancellationToken)
	{
		try
		{
			ArgumentException.ThrowIfNullOrEmpty(request.Email, nameof(request.Email));
			ArgumentException.ThrowIfNullOrEmpty(request.Password, nameof(request.Password));

			var foundUser = await _userRepository.GetByEmailAsync(request.Email,  cancellationToken);
			if (foundUser is null) throw new InvalidCredentialException();

			var isPasswordValid = PasswordHandler.VerifyPassword(request.Password, foundUser.Password);
			if (!isPasswordValid) throw new InvalidCredentialException();
			var token = _jwtAuthenticationHelper.GenerateJwtToken(foundUser);

			return new LoginDto { Token = token, Role = foundUser.Role.ToString() };
		}
		catch (InvalidCredentialException)
		{
			throw new AuthenticationException("Invalid email or password.");
		}
		catch (Exception)
		{
			throw new Exception("An unknown error has occurred.");
		}
		
	}
}