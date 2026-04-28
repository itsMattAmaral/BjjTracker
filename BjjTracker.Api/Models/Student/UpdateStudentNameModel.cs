using System.ComponentModel.DataAnnotations;
using BjjTracker.Application.Student.Commands.Actions;

namespace BjjTracker.Api.Models.Student;

public class UpdateStudentNameModel
{
	[Required]
	[Length(1, 100)]
	public required string FirstName { get; set; }
	[Required]
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