using BjjTracker.Api.Models.Class;
using BjjTracker.Application.Class.Queries.Dtos;
using BjjTracker.Application.Common.Dtos;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BjjTracker.Api.Controllers;

[ApiController]
[Produces("application/json")]
[Route("[controller]")]
public class ClassController(IMediator mediator) : ControllerBase
{
	private readonly IMediator _mediator = mediator ?? throw new ArgumentException(nameof(mediator));

	[HttpGet]
	[ProducesResponseType(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	[ProducesResponseType(StatusCodes.Status500InternalServerError)]
	public async Task<ActionResult<PagedResponseDto<ClassDto>>> SearchClasses(
		[FromQuery] SearchClassesModel model, CancellationToken cancellationToken =  default)
	{
		var query = model.GetFilter();
		var result = await _mediator.Send(query, cancellationToken);
		return Ok(result);
	}
	
	[HttpGet("{ClassId:int}")]
	[ProducesResponseType(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	[ProducesResponseType(StatusCodes.Status500InternalServerError)]
	public async Task<ActionResult<ClassDto>> GetSchoolById([FromRoute] GetClassByIdModel model, CancellationToken cancellationToken)
	{
		var query = model.GetFilter();
		var result = await _mediator.Send(query, cancellationToken);
		return Ok(result);
	}

	[HttpPost]
	[ProducesResponseType(StatusCodes.Status201Created)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	[ProducesResponseType(StatusCodes.Status500InternalServerError)]
	public async Task<IActionResult> RegisterClass([FromBody] RegisterClassModel model, CancellationToken cancellationToken)
	{
		model.ValidateDates();
		var command = model.GetCommand();
		await _mediator.Send(command, cancellationToken);
		return Created();
	}
}