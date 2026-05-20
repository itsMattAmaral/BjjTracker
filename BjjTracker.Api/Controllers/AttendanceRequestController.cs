using BjjTracker.Api.Models.AttendanceRequest;
using BjjTracker.Application.AttendanceRequest.Queries.Dtos;
using BjjTracker.Application.Common.Dtos;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BjjTracker.Api.Controllers;

[ApiController]
[Produces("application/json")]
[Route("[controller]")]
[Authorize]
public class AttendanceRequestController(IMediator mediator) : ControllerBase
{
	private readonly IMediator _mediator = mediator ?? throw new ArgumentException(nameof(mediator));
	
	[HttpGet]
	[ProducesResponseType(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	[ProducesResponseType(StatusCodes.Status500InternalServerError)]
	public async Task<ActionResult<PagedResponseDto<AttendanceRequestDto>>> SearchAttendanceRequests(
		[FromQuery] SearchAttendanceRequestsModel model, CancellationToken cancellationToken = default)
	{
		var  result = await _mediator.Send(model.GetFilter(), cancellationToken);
		return Ok(result);
	}
	
	[HttpGet("/byClassId/{classId:int}")]
	[ProducesResponseType(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	[ProducesResponseType(StatusCodes.Status500InternalServerError)]
	public async Task<ActionResult<IEnumerable<AttendanceRequestDto>>> GetAttendanceRequestsByClassId(
		[FromRoute] int classId, CancellationToken cancellationToken = default)
	{
		var model = new GetAttendancesByClassIdModel { ClassId = classId };
		var  result = await _mediator.Send(model.GetFilter(), cancellationToken);
		return Ok(result);
	}
	
	[HttpGet("/byStudentId/{studentId:int}")]
	[ProducesResponseType(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	[ProducesResponseType(StatusCodes.Status500InternalServerError)]
	public async Task<ActionResult<IEnumerable<AttendanceRequestDto>>> GetAttendanceRequestsByStudentId(
		[FromRoute] int studentId, CancellationToken cancellationToken = default)
	{
		var model = new GetAttendancesByStudentIdModel { StudentId = studentId };
		var  result = await _mediator.Send(model.GetFilter(), cancellationToken);
		return Ok(result);
	}
	
	[HttpGet("/byClassId/{classId:int}/byStudentId/{studentId:int}")]
	[ProducesResponseType(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	[ProducesResponseType(StatusCodes.Status500InternalServerError)]
	public async Task<ActionResult<IEnumerable<AttendanceRequestDto>>> GetAttendanceRequestByClassIdAndStudentId(
		[FromRoute] int classId, [FromRoute] int studentId, CancellationToken cancellationToken = default)
	{
		var model = new GetAttendenceRequest { ClassId = classId, StudentId = studentId };
		var  result = await _mediator.Send(model.GetFilter(), cancellationToken);
		return Ok(result);
	}
	
	[HttpPost]
	[ProducesResponseType(StatusCodes.Status201Created)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	[ProducesResponseType(StatusCodes.Status500InternalServerError)]
	public async Task<ActionResult> RegisterAttendanceRequest(
		[FromBody] RegisterAttendanceModel model, CancellationToken cancellationToken)
	{
		await _mediator.Send(model.GetCommand(), cancellationToken);
		return Created();
	}

	[HttpPatch("Approve")]
	[Authorize(Policy = "TeacherOnly")]
	[ProducesResponseType(StatusCodes.Status204NoContent)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	[ProducesResponseType(StatusCodes.Status500InternalServerError)]
	public async Task<ActionResult> ApproveAttendanceRequest(
		[FromBody] ApproveAttendanceModel model, CancellationToken cancellationToken)
	{
		await _mediator.Send(model.GetCommand(), cancellationToken);
		return NoContent();
	}
	
	[HttpDelete]
	[ProducesResponseType(StatusCodes.Status204NoContent)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	[ProducesResponseType(StatusCodes.Status500InternalServerError)]
	public async Task<ActionResult> DeleteAttendanceRequest(
		[FromBody] DeleteAttendanceModel model, CancellationToken cancellationToken)
	{
		await _mediator.Send(model.GetCommand(), cancellationToken);
		return NoContent();
	}
}