using System.ComponentModel.DataAnnotations;
using BjjTracker.Application.Teacher.Commands.Actions;

namespace BjjTracker.Api.Models.Teacher;

public class GraduateStudentModel
{
	[Required]
	[Range(1, int.MaxValue,  ErrorMessage = "StudentId must be greater than 0")]
	public int StudentId { get; set; }

	public GraduateStudentCommand GetCommand(int teacherId)
	{
		ArgumentOutOfRangeException.ThrowIfNegative(teacherId);
        return new GraduateStudentCommand(StudentId, teacherId);
	}
}