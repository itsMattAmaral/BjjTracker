using System.ComponentModel.DataAnnotations;
using BjjTracker.Application.AttendanceRequest.Commands.Actions;

namespace BjjTracker.Api.Models.AttendanceRequest;

public class RegisterAttendanceModel
{
	[Required]
	[Range(1, int.MaxValue, ErrorMessage = "Class Id must be greater than 0.")]
	public int ClassId { get; init; }
	
	[Range(1, int.MaxValue, ErrorMessage = "Student Id must be greater than 0.")]
	[Required]
	public int StudentId { get; init; }

	public RegisterAttendanceCommand GetCommand()
	{
		ArgumentOutOfRangeException.ThrowIfNegativeOrZero(ClassId);
		ArgumentOutOfRangeException.ThrowIfNegativeOrZero(StudentId);
		return new RegisterAttendanceCommand(ClassId, StudentId);
	}
}