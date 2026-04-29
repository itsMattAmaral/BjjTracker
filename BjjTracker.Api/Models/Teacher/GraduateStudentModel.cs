using System.ComponentModel.DataAnnotations;
using BjjTracker.Application.Teacher.Commands.Actions;
using Microsoft.AspNetCore.Mvc;

namespace BjjTracker.Api.Models.Teacher;

public class GraduateStudentModel
{
	[Required]
	[FromRoute]
	[Range(1, int.MaxValue,  ErrorMessage = "TeacherId must be greater than 0")]
	public int TeacherId { get; set; }
	
	[Required]
	[FromBody]
	[Range(1, int.MaxValue,  ErrorMessage = "StudentId must be greater than 0")]
	public int StudentId { get; set; }

	public GraduateStudentCommand GetCommand()
	{
        return new GraduateStudentCommand(StudentId, TeacherId);
	}
}