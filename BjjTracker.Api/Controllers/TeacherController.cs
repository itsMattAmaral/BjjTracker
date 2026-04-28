using BjjTracker.Api.Models.Teacher;
using BjjTracker.Application.Common.Dtos;
using BjjTracker.Application.Teacher.Queries.Dtos;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BjjTracker.Api.Controllers;

[ApiController]
[Produces("application/json")]
[Route("[controller]")]
public class TeacherController(IMediator mediator) : ControllerBase
{
	private readonly IMediator _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));

	[HttpGet]
	[ProducesResponseType(typeof(PagedResponseDto<TeacherDto>), StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	[ProducesResponseType(StatusCodes.Status500InternalServerError)]
	public async Task<ActionResult<PagedResponseDto<TeacherDto>>> SearchTeachers(
		[FromQuery] int page = 1,
		[FromQuery] int pageSize = 10,
		[FromQuery] string? searchTerm = null,
		[FromQuery] string? sortBy = null,
		[FromQuery] bool sortDescending = false,
		CancellationToken cancellationToken = default)
	{
		var model = new GetTeachersModel(page, pageSize, searchTerm, sortBy, sortDescending);
		var filter = model.GetFilter();
		var result = await _mediator.Send(filter, cancellationToken);
		return Ok(result);
	}
	
	[HttpGet("{teacherId:int}")]
	[ProducesResponseType(typeof(TeacherDto) ,StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	[ProducesResponseType(StatusCodes.Status500InternalServerError)]
	public async Task<ActionResult<TeacherDto>> GetTeacherById([FromRoute] int teacherId)
	{
		var model = new GetTeacherByIdModel { TeacherId = teacherId };
		var filter = model.GetFilter();
		var result = await _mediator.Send(filter);
		return Ok(result);
	}
	
	
	[HttpPatch("{teacherId:int}")]
	[ProducesResponseType(StatusCodes.Status204NoContent)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	[ProducesResponseType(StatusCodes.Status500InternalServerError)]
	public async Task<IActionResult> UpdateTeacherName([FromRoute]int teacherId,[FromBody]UpdateTeacherNameModel model)
	{
		ArgumentNullException.ThrowIfNull(model);
		var command = model.GetCommand(teacherId);
		await _mediator.Send(command);
		return NoContent();
	}
	
	[HttpPost("{teacherId:int}/graduateStudent")]
	[ProducesResponseType(StatusCodes.Status204NoContent)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	[ProducesResponseType(StatusCodes.Status500InternalServerError)]
	public async Task<IActionResult> GraduateStudent([FromBody]GraduateStudentModel model, int teacherId)
	{
		ArgumentNullException.ThrowIfNull(model);
		var command = model.GetCommand(teacherId);
		await _mediator.Send(command);
		return NoContent();
	}
	
}