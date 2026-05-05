using BjjTracker.Api.Models.Teacher;
using BjjTracker.Application.Common.Dtos;
using BjjTracker.Application.Teacher.Queries.Dtos;
using BjjTracker.Domain.Exceptions.School;
using BjjTracker.Domain.Exceptions.User;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BjjTracker.Api.Controllers;

[ApiController]
[Produces("application/json")]
[Route("[controller]")]
[Authorize(Policy = "TeacherOnly")]
public class TeacherController(IMediator mediator) : ControllerBase
{
	private readonly IMediator _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));

	[HttpGet]
	[ProducesResponseType(typeof(PagedResponseDto<TeacherDto>), StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	[ProducesResponseType(StatusCodes.Status500InternalServerError)]
	public async Task<ActionResult<PagedResponseDto<TeacherDto>>> SearchTeachers(
		SearchTeachersModel model,
		CancellationToken cancellationToken = default)
	{
		var filter = model.GetFilter();

		try
		{
			var result = await _mediator.Send(filter, cancellationToken);
			return Ok(result);
		}
		catch (Exception ex)
		{
			return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
		}
	}
	
	[HttpGet("{teacherId:int}")]
	[ProducesResponseType(typeof(TeacherDto) ,StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	[ProducesResponseType(StatusCodes.Status500InternalServerError)]
	public async Task<ActionResult<TeacherDto>> GetTeacherById([FromRoute] int teacherId, CancellationToken cancellationToken = default)
	{
		var model = new GetTeacherByIdModel {  TeacherId = teacherId };
		var filter = model.GetFilter();

		try
		{
			var result = await _mediator.Send(filter, cancellationToken);
			return Ok(result);
		}
		catch (UserNotFoundException ex)
		{
			return NotFound(ex.Message);
		}
		catch (Exception ex)
		{
			return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
		}
	}
	
	
	[HttpPatch("{teacherId:int}")]
	[ProducesResponseType(StatusCodes.Status204NoContent)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	[ProducesResponseType(StatusCodes.Status500InternalServerError)]
	public async Task<IActionResult> UpdateTeacherName([FromRoute]int teacherId, [FromBody]UpdateTeacherNameModel model, CancellationToken cancellationToken = default)
	{
		ArgumentNullException.ThrowIfNull(model);
		var command = model.GetCommand(teacherId);
		try
		{
			await _mediator.Send(command, cancellationToken);
			return NoContent();
		}
		catch (UserNotFoundException ex)
		{
			return NotFound(ex.Message);
		}
		catch (Exception ex)
		{
			return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
		}
	}
	
	[HttpPost("{teacherId:int}/graduateStudent")]
	[ProducesResponseType(StatusCodes.Status204NoContent)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	[ProducesResponseType(StatusCodes.Status500InternalServerError)]
	public async Task<IActionResult> GraduateStudent([FromRoute]int teacherId, [FromBody]GraduateStudentModel model, CancellationToken cancellationToken = default)
	{
		ArgumentNullException.ThrowIfNull(model);
		var command = model.GetCommand(teacherId);

		try
		{
			await _mediator.Send(command, cancellationToken);
			return NoContent();
		}
		catch (UserNotFoundException ex)
		{
			return NotFound(ex.Message);
		}
		catch (IsNotFromTheSameSchoolException ex)
		{
			return BadRequest(ex.Message);
		}
		catch (Exception ex)
		{
			return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
		}
	}
	
}