using BjjTracker.Api.Models.School;
using BjjTracker.Application.Common.Dtos;
using BjjTracker.Application.School.Queries.Dtos;
using BjjTracker.Domain.Exceptions.School;
using BjjTracker.Domain.Exceptions.Teacher;
using BjjTracker.Domain.Exceptions.User;
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
	
	[HttpGet("{schoolId:int}")]
	[ProducesResponseType(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	[ProducesResponseType(StatusCodes.Status500InternalServerError)]
	public async Task<ActionResult<SchoolDto>> GetSchoolById([FromRoute] int schoolId, CancellationToken cancellationToken)
	{
		var model = new GetSchoolByIdModel { SchoolId = schoolId };
		var query = model.GetFilter();

		try
		{
			var result = await _mediator.Send(query, cancellationToken);
			return Ok(result);
		}
		catch (SchoolNotFoundException ex)
		{
			return NotFound(ex.Message);
		}
		catch (Exception ex)
		{
			return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
		}
	}
	
	[HttpGet("document/{schoolDocument}")]
	[ProducesResponseType(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	[ProducesResponseType(StatusCodes.Status500InternalServerError)]
	public async Task<ActionResult<SchoolDto>> GetSchoolByDocument(string schoolDocument, CancellationToken cancellationToken)
	{
		var model = new  GetSchoolByDocumentModel { SchoolDocument = schoolDocument };
		var query = model.GetFilter();

		try
		{
			var result = await _mediator.Send(query, cancellationToken);
			return Ok(result);
		}
		catch (SchoolNotFoundException ex)
		{
			return NotFound(ex.Message);
		}
		catch (Exception ex)
		{
			return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
		}

	}
	
	[HttpPost]
	[Authorize(Policy = "TeacherOnly")]
	[ProducesResponseType(StatusCodes.Status201Created)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	[ProducesResponseType(StatusCodes.Status500InternalServerError)]
	public async Task<IActionResult> RegisterSchool([FromBody] RegisterSchoolModel model, CancellationToken cancellationToken)
	{
		var command = model.GetCommand();
		try
		{
			await _mediator.Send(command, cancellationToken);
			return Created();
		}
		catch (SchoolExistsException ex)
		{
			return BadRequest(ex.Message);
		}
		catch (Exception ex)
		{
			return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
		}
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
		var command = model.GetCommand(schoolId);
		try
		{
			await _mediator.Send(command, cancellationToken);
			return NoContent();
		}
		catch (UserNotFoundException ex)
		{
			return NotFound(ex.Message);
		}
		catch (SchoolNotFoundException ex)
		{
			return NotFound(ex.Message);
		}
		catch (TeacherOwnsAnotherSchoolException ex)
		{
			return BadRequest(ex.Message);
		}
		catch (TeacherAlreadyOwnsThisSchoolException ex)
		{
			return BadRequest(ex.Message);
		}
		catch (Exception ex)
		{
			return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
		}
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
		var command = model.GetCommand(schoolId);
		try
		{
			await _mediator.Send(command, cancellationToken);
			return NoContent();
		}
		catch (UserNotFoundException ex)
		{
			return NotFound(ex.Message);
		}
		catch (SchoolNotFoundException ex)
		{
			return NotFound(ex.Message);
		}
		catch (Exception ex)
		{
			return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
		}
	}
}