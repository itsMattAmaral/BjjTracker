using System.ComponentModel.DataAnnotations;
using BjjTracker.Application.Class.Commands.Actions;

namespace BjjTracker.Api.Models.Class;

public class RegisterClassModel
{
	[Required]
	[Range(1, int.MaxValue,  ErrorMessage = "SchoolId must be greater than 0.")]
	public required int SchoolId { get; set; }
	[Required]
	[Range(1, int.MaxValue, ErrorMessage = "TeacherId must be greater than 0.")]
	public required int TeacherId { get; set; }
	[Required]
	public DateTime BeginDate { get; set; } =  DateTime.UtcNow;
	[Required]
	public DateTime EndDate { get; set; } = DateTime.UtcNow.AddHours(1);
	
	public RegisterClassCommand GetCommand()
	{
		return new RegisterClassCommand(
			SchoolId,
			TeacherId,
			BeginDate,
			EndDate
		);
	}
	
	public void ValidateDates()
	{
		if  (BeginDate > EndDate || BeginDate < DateTime.Today || EndDate < DateTime.Today)
		{
			throw new ValidationException();
		}

	}
}