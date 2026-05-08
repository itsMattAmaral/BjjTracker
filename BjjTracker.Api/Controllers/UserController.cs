using System.Security.Authentication;
using BjjTracker.Api.Models.Authentication;
using BjjTracker.Application.Common.Dtos;
using BjjTracker.Domain.Exceptions.User;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BjjTracker.Api.Controllers;

[ApiController]
[Produces("application/json")]
public class UserController(IMediator mediator) : ControllerBase
{
	private readonly IMediator _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
	
	[HttpPost("login")]
	[ProducesResponseType(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	[ProducesResponseType(StatusCodes.Status500InternalServerError)]
	public async Task<ActionResult<LoginDto>> Login([FromBody] UserLoginViewModel model)
	{
		var command = model.GetCommand();

		try
		{
			var result = await _mediator.Send(command);
			return Ok(result);
		}
		catch (InvalidCredentialException ex)
		{
			if (ex.InnerException != null)
				return BadRequest(ex.InnerException.Message);
			
			return BadRequest(ex.Message);
		}
		catch (Exception ex)
		{
			if (ex.InnerException != null)
				return StatusCode(StatusCodes.Status500InternalServerError, ex.InnerException.Message);
			
			return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
		}

	}
	
	[HttpPost("register")]
	[ProducesResponseType(StatusCodes.Status201Created)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	[ProducesResponseType(StatusCodes.Status500InternalServerError)]
	public async Task<IActionResult> Register([FromBody] UserRegisterViewModel model)
	{
		var command = model.GetCommand();

		try
		{
			await _mediator.Send(command);
			return Created();
		}
		catch (ThisEmailAlreadyExistsException ex)
		{
			if (ex.InnerException != null)
				return BadRequest(ex.InnerException.Message);
			
			return BadRequest(ex.Message);
		}
		catch (RoleNotFoundException ex)
		{
			if (ex.InnerException != null)
				return BadRequest(ex.InnerException.Message);
			
			return BadRequest(ex.Message);
		}
		catch (Exception ex)
		{
			if (ex.InnerException != null)
				return StatusCode(StatusCodes.Status500InternalServerError, ex.InnerException.Message);
			
			return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
		}
	}
}