using BjjTracker.Api.Models.Authentication;
using BjjTracker.Application.Common.Dtos;
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
		var result = await _mediator.Send(model.GetCommand());
		return Ok(result);

	}
	
	[HttpPost("register")]
	[ProducesResponseType(StatusCodes.Status201Created)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	[ProducesResponseType(StatusCodes.Status500InternalServerError)]
	public async Task<IActionResult> Register([FromBody] UserRegisterViewModel model)
	{
		await _mediator.Send(model.GetCommand());
		return Created();
	}
}