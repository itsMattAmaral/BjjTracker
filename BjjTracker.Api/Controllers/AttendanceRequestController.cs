using BjjTracker.Api.Models.AttendanceRequest;
using BjjTracker.Application.AttendanceRequest.Queries.Dtos;
using BjjTracker.Application.Common.Dtos;
using BjjTracker.Domain.Exceptions.AttendanceRequest;
using BjjTracker.Domain.Exceptions.Class;
using BjjTracker.Domain.Exceptions.User;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BjjTracker.Api.Controllers;

[ApiController]
[Produces("application/json")]
[Route("[controller]")]
[Authorize(Policy = "AnyUser")]
public class AttendanceRequestController(IMediator mediator) : ControllerBase
{
	private readonly IMediator _mediator = mediator ?? throw new ArgumentException(nameof(mediator));
	
	[HttpGet]
	[ProducesResponseType(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	[ProducesResponseType(StatusCodes.Status500InternalServerError)]
	public async Task<ActionResult<PagedResponseDto<AttendanceRequestDto>>> SearchAttendanceRequests([FromQuery] SearchAttendanceRequestsModel model, CancellationToken cancellationToken = default)
	{
		var query = model.GetFilter();
		try
		{
			var  result = await _mediator.Send(query, cancellationToken);
			return Ok(result);
		}
		catch (Exception ex)
		{
			return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
		}
	}
	
	[HttpGet("/byClassId/{classId:int}")]
	[ProducesResponseType(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	[ProducesResponseType(StatusCodes.Status500InternalServerError)]
	public async Task<ActionResult<IEnumerable<AttendanceRequestDto>>> GetAttendanceRequestsByClassId([FromRoute] int classId, CancellationToken cancellationToken = default)
	{
		var model = new GetAttendancesByClassIdModel { ClassId = classId };
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
	
	[HttpGet("/byStudentId/{studentId:int}")]
	[ProducesResponseType(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	[ProducesResponseType(StatusCodes.Status500InternalServerError)]
	public async Task<ActionResult<IEnumerable<AttendanceRequestDto>>> GetAttendanceRequestsByStudentId([FromRoute] int studentId, CancellationToken cancellationToken = default)
	{
		var model = new GetAttendancesByStudentIdModel { StudentId = studentId };
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
	
	[HttpGet("/byClassId/{classId:int}/byStudentId/{studentId:int}")]
	[ProducesResponseType(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	[ProducesResponseType(StatusCodes.Status500InternalServerError)]
	public async Task<ActionResult<IEnumerable<AttendanceRequestDto>>> GetAttendanceRequestByClassIdAndStudentId([FromRoute] int classId, [FromRoute] int studentId, CancellationToken cancellationToken = default)
	{
		var model = new GetAttendenceRequest { ClassId = classId, StudentId = studentId };
		var query = model.GetFilter();
		try
		{
			var result = await _mediator.Send(query, cancellationToken);
			return Ok(result);
		}
		catch (AttendanceRequestNotFoundException ex)
		{
			return NotFound(ex.Message);
		}
		catch (Exception ex)
		{
			return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
		}
	}
	
	[HttpPost]
	[ProducesResponseType(StatusCodes.Status201Created)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	[ProducesResponseType(StatusCodes.Status500InternalServerError)]
	public async Task<ActionResult> RegisterAttendanceRequest([FromBody] RegisterAttendanceModel model, CancellationToken cancellationToken)
	{
		var command = model.GetCommand();

		try
		{
			await _mediator.Send(command, cancellationToken);
			return Created();
		}
		catch (UserNotFoundException ex)
		{
			return NotFound(ex.Message);
		}
		catch (ClassNotFoundException ex)
		{
			return NotFound(ex.Message);
		}
		catch (UserAlreadyOpenedARequestForThisClassException ex)
		{
			return BadRequest(ex.Message);
		}
		catch (Exception ex)
		{
			return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
		}
	}

	[HttpPatch("Approve")]
	[Authorize(Policy = "TeacherOnly")]
	[ProducesResponseType(StatusCodes.Status204NoContent)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	[ProducesResponseType(StatusCodes.Status500InternalServerError)]
	public async Task<ActionResult> ApproveAttendanceRequest([FromBody] ApproveAttendanceModel model, CancellationToken cancellationToken)
	{
		var command = model.GetCommand();

		try
		{
			await _mediator.Send(command, cancellationToken);
			return NoContent();
		}
		catch (UserNotFoundException ex)
		{
			return NotFound(ex.Message);
		}
		catch (AttendanceRequestNotFoundException ex)
		{
			return NotFound(ex.Message);
		}
		catch (Exception ex)
		{
			return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
		}
	}
	
	[HttpDelete]
	[ProducesResponseType(StatusCodes.Status204NoContent)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	[ProducesResponseType(StatusCodes.Status500InternalServerError)]
	public async Task<ActionResult> DeleteAttendanceRequest([FromBody] DeleteAttendanceModel model, CancellationToken cancellationToken)
	{
		var command = model.GetCommand();

		try
		{
			await _mediator.Send(command, cancellationToken);
			return NoContent();
		}
		catch (AttendanceRequestNotFoundException ex)
		{
			return NotFound(ex.Message);
		}
		catch (Exception ex)
		{
			return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
		}
	}
}