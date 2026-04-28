using System.ComponentModel.DataAnnotations;
using BjjTracker.Application.Teacher.Commands.Actions;

namespace BjjTracker.Api.Models.Teacher;

public class UpdateTeacherNameModel
{
	[Required]
	[Length(1, 100)]
	public required string FirstName { get; set; }
	
	[Required]
	[Length(1, 100)]
	public required string LastName { get; set; }
	
	public UpdateTeacherNameCommand GetCommand(int teacherId)
	{
		ArgumentOutOfRangeException.ThrowIfNegative(teacherId);
		return new UpdateTeacherNameCommand(FirstName, LastName)
		{
			TeacherId = teacherId
		};
	}
}