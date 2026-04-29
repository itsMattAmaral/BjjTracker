using System.ComponentModel.DataAnnotations;
using BjjTracker.Application.Student.Commands.Actions;
using Microsoft.AspNetCore.Mvc;

namespace BjjTracker.Api.Models.Student;

public class UpdateStudentNameModel
{
	[Required]
	[FromRoute]
	[Range(1, int.MaxValue,  ErrorMessage = "StudentId must be greater than or equal to 1.")]
	public int StudentId { get; set; }
	
	[Required]
	[FromBody]
	[Length(1, 100)]
	public required string FirstName { get; set; }
	[Required]
	[FromBody]
	[Length(1, 100)]
	public required string LastName { get; set; }
	
	public UpdateStudentNameCommand GetCommand()
	{
		ArgumentOutOfRangeException.ThrowIfNegative(StudentId);
		return new UpdateStudentNameCommand(FirstName, LastName)
		{
			StudentId = StudentId
		};
	}
}