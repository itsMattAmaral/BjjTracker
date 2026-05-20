using MediatR;
using Microsoft.AspNetCore.Mvc;
using BjjTracker.Api.Models.Student;
using BjjTracker.Application.Common.Dtos;
using BjjTracker.Application.Student.Queries.Dtos;
using BjjTracker.Domain.Exceptions.School;
using BjjTracker.Domain.Exceptions.User;
using Microsoft.AspNetCore.Authorization;

namespace BjjTracker.Api.Controllers;


[ApiController]
[Produces("application/json")]
[Route("[controller]")]
[Authorize]
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
		var result = await _mediator.Send(model.GetFilter(), cancellationToken);
		return Ok(result);
	}
	
	[HttpGet("{studentId:int}")]
	[ProducesResponseType(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	[ProducesResponseType(StatusCodes.Status500InternalServerError)]
	public async Task<ActionResult<StudentDto>> GetStudentById([FromRoute] int studentId, CancellationToken cancellationToken = default)
	{
		var model = new GetStudentByIdModel { StudentId = studentId };
		var result = await _mediator.Send(model.GetFilter(), cancellationToken);
		return Ok(result);

	}
	
	[Authorize(Policy = "StudentOnly")]
	[HttpPatch("{studentId:int}/school")]
	[ProducesResponseType(StatusCodes.Status204NoContent)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	[ProducesResponseType(StatusCodes.Status500InternalServerError)]
	public async Task<IActionResult> UpdateStudentSchoolId([FromRoute]int studentId, [FromBody]UpdateStudentSchoolIdModel model, CancellationToken cancellationToken = default)
	{
		await _mediator.Send(model.GetCommand(studentId), cancellationToken);
		return NoContent();
	}
	
	[Authorize(Policy = "StudentOnly")]
	[HttpPatch("{studentId:int}/name")]
	[ProducesResponseType(StatusCodes.Status204NoContent)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	[ProducesResponseType(StatusCodes.Status500InternalServerError)]
	public async Task<IActionResult> UpdateStudentNameId([FromRoute]int studentId, [FromBody]UpdateStudentNameModel model, CancellationToken cancellationToken = default)
	{
		await _mediator.Send(model.GetCommand(studentId), cancellationToken);
		return NoContent();
	}
}