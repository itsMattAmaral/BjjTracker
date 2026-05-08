using System.ComponentModel.DataAnnotations;
using BjjTracker.Application.School.Commands.Actions;

namespace BjjTracker.Api.Models.School;

public class RemoveOwnerModel
{
	[Required]
	[Range(1, int.MaxValue,  ErrorMessage = "TeacherId must be greater than or equal to 1.")]
	public int TeacherId { get; set; }

	public RemoveOwnerCommand GetCommand(int schoolId)
	{
		ArgumentOutOfRangeException.ThrowIfNegativeOrZero(TeacherId);
		ArgumentOutOfRangeException.ThrowIfNegativeOrZero(schoolId);
		return new RemoveOwnerCommand(TeacherId, schoolId);
	}
}