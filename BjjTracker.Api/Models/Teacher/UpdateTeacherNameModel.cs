using System.ComponentModel.DataAnnotations;
using BjjTracker.Application.Teacher.Commands.Actions;
using Microsoft.AspNetCore.Mvc;

namespace BjjTracker.Api.Models.Teacher;

public class UpdateTeacherNameModel
{
	[Required]
	[FromRoute]
	[Range(1, int.MaxValue,  ErrorMessage = "TeacherId must be greater than or equal to 1.")]
	public int TeacherId { get; set; }
	
	[Required]
	[Length(1, 100)]
	public required string FirstName { get; set; }
	
	[Required]
	[Length(1, 100)]
	public required string LastName { get; set; }
	
	public UpdateTeacherNameCommand GetCommand()
	{
		ArgumentOutOfRangeException.ThrowIfNegative(TeacherId);
		return new UpdateTeacherNameCommand(FirstName, LastName)
		{
			TeacherId = TeacherId
		};
	}
}