using System.ComponentModel.DataAnnotations;
using BjjTracker.Application.AttendanceRequest.Queries.Filters;

namespace BjjTracker.Api.Models.AttendanceRequest;

public class GetAttendancesByClassIdModel
{
	[Required]
	[Range(1, int.MaxValue, ErrorMessage = "ClassId must be greater than 0.")]
	public int ClassId { get; init; }
	
	public GetAttendancesByClassIdFilter GetFilter()
	{
		ArgumentOutOfRangeException.ThrowIfNegativeOrZero(ClassId);
		return new GetAttendancesByClassIdFilter(ClassId);
	}
}