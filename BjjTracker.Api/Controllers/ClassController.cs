using BjjTracker.Api.Models.Class;
using BjjTracker.Application.Class.Queries.Dtos;
using BjjTracker.Application.Common.Dtos;
using BjjTracker.Domain.Exceptions.Class;
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
	[ProducesResponseType(StatusCodes.Status500InternalServerError)]
	public async Task<ActionResult<PagedResponseDto<ClassDto>>> SearchClasses(SearchClassesModel model, CancellationToken cancellationToken =  default)
	{
		var query = model.GetFilter();

		try
		{
			var result = await _mediator.Send(query, cancellationToken);
			return Ok(result);
		}
		catch (Exception ex)
		{
			return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
		}
	}
	
	[HttpGet("{classId:int}")]
	[ProducesResponseType(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	[ProducesResponseType(StatusCodes.Status500InternalServerError)]
	public async Task<ActionResult<ClassDto>> GetSchoolById([FromRoute]int classId, CancellationToken cancellationToken)
	{
		var model = new GetClassByIdModel{ ClassId = classId };
		var query = model.GetFilter();

		try
		{
			var result = await _mediator.Send(query, cancellationToken);
			return Ok(result);
		}
		catch (ClassNotFoundException ex)
		{
			return NotFound(ex.Message);
		}
		catch (Exception ex)
		{
			return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
		}
	}

	[HttpPost]
	[ProducesResponseType(StatusCodes.Status201Created)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	[ProducesResponseType(StatusCodes.Status500InternalServerError)]
	public async Task<IActionResult> RegisterClass([FromBody] RegisterClassModel model, CancellationToken cancellationToken)
	{
		model.ValidateDates();
		var command = model.GetCommand();

		try
		{
			await _mediator.Send(command, cancellationToken);
			return Created();
		}
		catch (ClassDateConflictException ex)
		{
			return BadRequest(ex.Message);
		}
		catch (Exception ex)
		{
			return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
		}
	}
}