using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using BjjTracker.Application.Teacher.Commands.Actions;
using Microsoft.AspNetCore.Mvc;

namespace BjjTracker.Api.Models.Teacher;

public class GraduateStudentModel
{
	[Required]
	[FromBody]
	[Range(1, int.MaxValue,  ErrorMessage = "TeacherId must be greater than 0")]
	public int StudentId { get; set; }

	public GraduateStudentCommand GetCommand(int teacherId)
	{
        ArgumentOutOfRangeException.ThrowIfLessThan(teacherId, 1);
        return new GraduateStudentCommand(StudentId, teacherId);
	}
}