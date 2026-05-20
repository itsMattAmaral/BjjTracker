using BjjTracker.Api.Models.Class;
using BjjTracker.Application.Class.Queries.Dtos;
using BjjTracker.Application.Common.Dtos;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BjjTracker.Api.Controllers;

[ApiController]
[Produces("application/json")]
[Route("[controller]")]
[Authorize(Policy = "TeacherOnly")]
public class ClassController(IMediator mediator) : ControllerBase
{
	private readonly IMediator _mediator = mediator ?? throw new ArgumentException(nameof(mediator));

	[HttpGet]
	[ProducesResponseType(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status500InternalServerError)]
	public async Task<ActionResult<PagedResponseDto<ClassDto>>> SearchClasses(SearchClassesModel model, CancellationToken cancellationToken =  default)
	{
		var result = await _mediator.Send(model.GetFilter(), cancellationToken);
		return Ok(result);
	}
	
	[HttpGet("{classId:int}")]
	[ProducesResponseType(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	[ProducesResponseType(StatusCodes.Status500InternalServerError)]
	public async Task<ActionResult<ClassDto>> GetSchoolById([FromRoute]int classId, CancellationToken cancellationToken)
	{
		var model = new GetClassByIdModel{ ClassId = classId };
		var result = await _mediator.Send(model.GetFilter(), cancellationToken);
		return Ok(result);
	}

	[HttpPost]
	[ProducesResponseType(StatusCodes.Status201Created)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	[ProducesResponseType(StatusCodes.Status500InternalServerError)]
	public async Task<IActionResult> RegisterClass([FromBody] RegisterClassModel model, CancellationToken cancellationToken)
	{
		model.ValidateDates();
		await _mediator.Send(model.GetCommand(), cancellationToken);
		return Created();
	}
}