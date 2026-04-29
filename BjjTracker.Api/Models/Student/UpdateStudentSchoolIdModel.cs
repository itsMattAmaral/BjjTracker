using System.ComponentModel.DataAnnotations;
using BjjTracker.Application.Student.Commands.Actions;
using Microsoft.AspNetCore.Mvc;

namespace BjjTracker.Api.Models.Student;

public class UpdateStudentSchoolIdModel
{
	[Required]
	[FromRoute]
	[Range(1, int.MaxValue,  ErrorMessage = "StudentId must be greater than or equal to 1.")]
	public int StudentId { get; set; }
	
	[Required]
	[Range(1, int.MaxValue,  ErrorMessage = "SchoolId must be greater than 0")]
	public int SchoolId { get; set; }

	public UpdateStudentSchoolIdCommand GetCommand()
	{
		ArgumentOutOfRangeException.ThrowIfNegative(StudentId);
		return new UpdateStudentSchoolIdCommand(SchoolId)
		{
			StudentId = StudentId
		};
	}
}