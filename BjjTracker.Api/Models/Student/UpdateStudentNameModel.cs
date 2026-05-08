using System.ComponentModel.DataAnnotations;
using BjjTracker.Application.Student.Commands.Actions;
using Microsoft.AspNetCore.Mvc;

namespace BjjTracker.Api.Models.Student;

public class UpdateStudentNameModel
{
	[Required]
	[FromBody]
	[Length(1, 100)]
	public required string FirstName { get; set; }
	[Required]
	[FromBody]
	[Length(1, 100)]
	public required string LastName { get; set; }
	
	public UpdateStudentNameCommand GetCommand(int studentId)
	{
		ArgumentOutOfRangeException.ThrowIfNegative(studentId);
		return new UpdateStudentNameCommand(FirstName, LastName)
		{
			StudentId = studentId
		};
	}
}