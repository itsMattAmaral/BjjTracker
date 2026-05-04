using MediatR;
using Microsoft.AspNetCore.Mvc;
using BjjTracker.Api.Models.Student;
using BjjTracker.Application.Common.Dtos;
using BjjTracker.Application.Student.Queries.Dtos;
using BjjTracker.Domain.Exceptions.School;
using BjjTracker.Domain.Exceptions.User;

namespace BjjTracker.Api.Controllers;


[ApiController]
[Produces("application/json")]
[Route("[controller]")]
public class StudentController(IMediator mediator) : ControllerBase
{
	private readonly IMediator _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));

	[HttpGet]
	[ProducesResponseType(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	[ProducesResponseType(StatusCodes.Status500InternalServerError)]
	public async Task<ActionResult<PagedResponseDto<StudentDto>>> SearchStudents(
		SearchStudentsModel model,
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
	
	[HttpGet("{studentId:int}")]
	[ProducesResponseType(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	[ProducesResponseType(StatusCodes.Status500InternalServerError)]
	public async Task<ActionResult<StudentDto>> GetStudentById([FromRoute] int studentId, CancellationToken cancellationToken = default)
	{
		var model = new GetStudentByIdModel { StudentId = studentId };
		var query = model.GetFilter();

		try
		{
			var result = await _mediator.Send(query,  cancellationToken);
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
	
	[HttpPatch("{studentId:int}/school")]
	[ProducesResponseType(StatusCodes.Status204NoContent)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	[ProducesResponseType(StatusCodes.Status500InternalServerError)]
	public async Task<IActionResult> UpdateStudentSchoolId([FromRoute]int studentId, [FromBody]UpdateStudentSchoolIdModel model, CancellationToken cancellationToken = default)
	{
		var command = model.GetCommand(studentId);

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
	
	[HttpPatch("{studentId:int}/name")]
	[ProducesResponseType(StatusCodes.Status204NoContent)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	[ProducesResponseType(StatusCodes.Status500InternalServerError)]
	public async Task<IActionResult> UpdateStudentNameId([FromRoute]int studentId, [FromBody]UpdateStudentNameModel model, CancellationToken cancellationToken = default)
	{
		var command = model.GetCommand(studentId);

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
}