using System.ComponentModel.DataAnnotations;
using BjjTracker.Application.AttendanceRequest.Queries.Filters;

namespace BjjTracker.Api.Models.AttendanceRequest;

public class GetAttendenceRequest
{
	[Required]
	public int ClassId { get; init; }
	[Required]
	public int StudentId { get; init; }
	
	public GetAttendanceRequestFilter GetFilter()
	{
		ArgumentOutOfRangeException.ThrowIfNegativeOrZero(ClassId);
		ArgumentOutOfRangeException.ThrowIfNegativeOrZero(StudentId);
		return new GetAttendanceRequestFilter(StudentId, ClassId);
	}
}