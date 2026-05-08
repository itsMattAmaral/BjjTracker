using System.ComponentModel.DataAnnotations;
using BjjTracker.Application.AttendanceRequest.Queries.Filters;

namespace BjjTracker.Api.Models.AttendanceRequest;

public class GetAttendancesByStudentIdModel
{
	[Required]
	[Range(1, int.MaxValue, ErrorMessage = "StudentId must be greater than 0.")]
	public int StudentId { get; init; }
	
	public GetAttendancesByStudentIdFilter GetFilter()
	{
		ArgumentOutOfRangeException.ThrowIfNegativeOrZero(StudentId);
		return new GetAttendancesByStudentIdFilter(StudentId);
	}
}