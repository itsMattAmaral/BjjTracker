using BjjTracker.Api.Models.School;
using BjjTracker.Application.Common.Dtos;
using BjjTracker.Application.School.Queries.Dtos;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BjjTracker.Api.Controllers;

[ApiController]
[Produces("application/json")]
[Route("[controller]")]

public class SchoolController(IMediator mediator) : ControllerBase
{
	private readonly IMediator _mediator = mediator ?? throw new ArgumentException(nameof(mediator));
	
	[HttpGet]
	[ProducesResponseType(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	[ProducesResponseType(StatusCodes.Status500InternalServerError)]
	public async Task<ActionResult<PagedResponseDto<SchoolDto>>> SearchSchools(
		SearchSchoolsModel model,
		CancellationToken cancellationToken = default)
	{
		var query = model.GetFilter();
		var result = await _mediator.Send(query, cancellationToken);
		return Ok(result);
	}
	
	[HttpGet("{schoolId:int}")]
	[ProducesResponseType(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	[ProducesResponseType(StatusCodes.Status500InternalServerError)]
	public async Task<ActionResult<SchoolDto>> GetSchoolById(GetSchoolByIdModel model, CancellationToken cancellationToken)
	{
		var query = model.GetFilter();
		var result = await _mediator.Send(query, cancellationToken);
		return Ok(result);
	}
	
	[HttpGet("document/{schoolDocument}")]
	[ProducesResponseType(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	[ProducesResponseType(StatusCodes.Status500InternalServerError)]
	public async Task<ActionResult<SchoolDto>> GetSchoolById(GetSchoolByDocumentModel model, CancellationToken cancellationToken)
	{
		var query = model.GetFilter();
		var result = await _mediator.Send(query, cancellationToken);
		return Ok(result);
	}
	
	[HttpPost]
	[ProducesResponseType(StatusCodes.Status201Created)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	[ProducesResponseType(StatusCodes.Status500InternalServerError)]
	public async Task<IActionResult> RegisterSchool([FromBody] RegisterSchoolModel model, CancellationToken cancellationToken)
	{
		var command = model.GetCommand();
		await _mediator.Send(command, cancellationToken);
		return Created();
	}
}