using BjjTracker.Api.Models.Teacher;
using BjjTracker.Application.Common.Dtos;
using BjjTracker.Application.Teacher.Queries.Dtos;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BjjTracker.Api.Controllers;

[ApiController]
[Produces("application/json")]
[Route("[controller]")]
[Authorize]
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
		var result = await _mediator.Send(model.GetFilter(), cancellationToken);
		return Ok(result);
	}
	
	[HttpGet("{teacherId:int}")]
	[ProducesResponseType(typeof(TeacherDto) ,StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	[ProducesResponseType(StatusCodes.Status500InternalServerError)]
	public async Task<ActionResult<TeacherDto>> GetTeacherById([FromRoute] int teacherId, CancellationToken cancellationToken = default)
	{
		var model = new GetTeacherByIdModel {  TeacherId = teacherId };
		var result = await _mediator.Send(model.GetFilter(), cancellationToken);
		return Ok(result);
	}
	
	[Authorize(Policy = "TeacherOnly")]
	[HttpPatch("{teacherId:int}")]
	[ProducesResponseType(StatusCodes.Status204NoContent)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	[ProducesResponseType(StatusCodes.Status500InternalServerError)]
	public async Task<IActionResult> UpdateTeacherName([FromRoute]int teacherId, [FromBody]UpdateTeacherNameModel model, CancellationToken cancellationToken = default)
	{
		await _mediator.Send(model.GetCommand(teacherId), cancellationToken);
		return NoContent();
	}
	
	[Authorize(Policy = "TeacherOnly")]
	[HttpPost("{teacherId:int}/graduateStudent")]
	[ProducesResponseType(StatusCodes.Status204NoContent)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	[ProducesResponseType(StatusCodes.Status500InternalServerError)]
	public async Task<IActionResult> GraduateStudent([FromRoute]int teacherId, [FromBody]GraduateStudentModel model, CancellationToken cancellationToken = default)
	{
		await _mediator.Send(model.GetCommand(teacherId), cancellationToken);
		return NoContent();
	}
	
}