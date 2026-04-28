using MediatR;
using Microsoft.AspNetCore.Mvc;
using BjjTracker.Api.Models.Student;
using BjjTracker.Application.Common.Dtos;
using BjjTracker.Application.Student.Queries.Dtos;

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
		[FromQuery] int page = 1,
		[FromQuery] int pageSize = 10,
		[FromQuery] string? searchTerm = null,
		[FromQuery] string? sortBy = null,
		[FromQuery] bool sortDescending = false,
		CancellationToken cancellationToken = default)
	{
		var model = new SearchStudentsModel(page, pageSize, searchTerm, sortBy, sortDescending);
		var query = model.GetFilter();
		var result = await _mediator.Send(query, cancellationToken);
		return Ok(result);
	}
	
	[HttpGet("{studentId:int}")]
	[ProducesResponseType(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	[ProducesResponseType(StatusCodes.Status500InternalServerError)]
	public async Task<ActionResult<StudentDto>> GetStudentById([FromRoute] int studentId, CancellationToken cancellationToken)
	{
		var model = new GetStudentByIdModel { Id = studentId };
		var query = model.GetFilter();
		var result = await _mediator.Send(query, cancellationToken);
		return Ok(result);
	}
	
	[HttpPatch("{studentId:int}/school")]
	[ProducesResponseType(StatusCodes.Status204NoContent)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	[ProducesResponseType(StatusCodes.Status500InternalServerError)]
	public async Task<IActionResult> UpdateStudentSchoolId(int studentId, [FromBody] UpdateStudentSchoolIdModel model)
	{
		var command = model.GetCommand(studentId);
		await _mediator.Send(command);
		return NoContent();
	}
	
	[HttpPatch("{studentId:int}/name")]
	[ProducesResponseType(StatusCodes.Status204NoContent)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	[ProducesResponseType(StatusCodes.Status500InternalServerError)]
	public async Task<IActionResult> UpdateStudentNameId(int studentId, [FromBody] UpdateStudentNameModel model)
	{
		var command = model.GetCommand(studentId);
		await _mediator.Send(command);
		return NoContent();
	}
}