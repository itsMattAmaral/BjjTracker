using BjjTracker.Api.Models.School;
using BjjTracker.Application.Common.Dtos;
using BjjTracker.Application.School.Queries.Dtos;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BjjTracker.Api.Controllers;

[ApiController]
[Produces("application/json")]
[Route("[controller]")]
[Authorize]
public class SchoolController(IMediator mediator) : ControllerBase
{
	private readonly IMediator _mediator = mediator ?? throw new ArgumentException(nameof(mediator));
	
	[HttpGet]
	[ProducesResponseType(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	[ProducesResponseType(StatusCodes.Status500InternalServerError)]
	public async Task<ActionResult<PagedResponseDto<SchoolDto>>> SearchSchools(
		SearchSchoolsModel model,
		CancellationToken cancellationToken = default)
	{
		var result = await _mediator.Send(model.GetFilter(), cancellationToken);
		return Ok(result);
	}
	
	[HttpGet("{schoolId:int}")]
	[ProducesResponseType(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	[ProducesResponseType(StatusCodes.Status500InternalServerError)]
	public async Task<ActionResult<SchoolDto>> GetSchoolById([FromRoute] int schoolId, CancellationToken cancellationToken)
	{
		var model = new GetSchoolByIdModel { SchoolId = schoolId };
		var result = await _mediator.Send(model.GetFilter(), cancellationToken);
		return Ok(result);
	}
	
	[HttpGet("document/{schoolDocument}")]
	[ProducesResponseType(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	[ProducesResponseType(StatusCodes.Status500InternalServerError)]
	public async Task<ActionResult<SchoolDto>> GetSchoolByDocument(string schoolDocument, CancellationToken cancellationToken)
	{
		var model = new  GetSchoolByDocumentModel { SchoolDocument = schoolDocument };
		var result = await _mediator.Send(model.GetFilter(), cancellationToken);
		return Ok(result);

	}
	
	[HttpPost]
	[Authorize(Policy = "TeacherOnly")]
	[ProducesResponseType(StatusCodes.Status201Created)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	[ProducesResponseType(StatusCodes.Status500InternalServerError)]
	public async Task<IActionResult> RegisterSchool([FromBody] RegisterSchoolModel model, CancellationToken cancellationToken)
	{
		await _mediator.Send(model.GetCommand(), cancellationToken);
		return Created();
	}

	[HttpPatch("{schoolId:int}/AddOwner")]
	[Authorize(Policy = "SchoolOwner")]
	[ProducesResponseType(StatusCodes.Status204NoContent)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	[ProducesResponseType(StatusCodes.Status500InternalServerError)]
	public async Task<IActionResult> AddOwner([FromRoute] int schoolId, [FromBody] AddOwnerModel model,
		CancellationToken cancellationToken)
	{
		await _mediator.Send(model.GetCommand(schoolId), cancellationToken);
		return NoContent();
	}
	
	[HttpPatch("{schoolId:int}/RemoveOwner")]
	[Authorize(Policy = "SchoolOwner")]
	[ProducesResponseType(StatusCodes.Status204NoContent)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	[ProducesResponseType(StatusCodes.Status500InternalServerError)]
	public async Task<IActionResult> RemoveOwner([FromRoute] int schoolId, [FromBody] RemoveOwnerModel model,
		CancellationToken cancellationToken)
	{
		await _mediator.Send(model.GetCommand(schoolId), cancellationToken);
		return NoContent();
	}
}