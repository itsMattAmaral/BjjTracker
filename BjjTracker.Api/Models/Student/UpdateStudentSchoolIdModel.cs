using System.ComponentModel.DataAnnotations;
using BjjTracker.Application.Student.Commands.Actions;

namespace BjjTracker.Api.Models.Student;

public class UpdateStudentSchoolIdModel
{
	[Required]
	[Range(1, int.MaxValue,  ErrorMessage = "SchoolId must be greater than 0")]
	public int SchoolId { get; set; }

	public UpdateStudentSchoolIdCommand GetCommand(int studentId)
	{
		ArgumentOutOfRangeException.ThrowIfNegative(studentId);
		return new UpdateStudentSchoolIdCommand(SchoolId)
		{
			Id = studentId
		};
	}
}