using System.ComponentModel.DataAnnotations;
using BjjTracker.Application.School.Commands.Actions;
using BjjTracker.Domain.Entities;
using DocumentValidator;

namespace BjjTracker.Api.Models.School;

public class RegisterSchoolModel : IValidatableObject
{
	[Required]
	public required string Document { get; set; }
	[Required]
	[Length(3, 100, ErrorMessage = "Name must be between 3 and 100 characters.")]
	public required string Name { get; set; }
	[Required]
	[EmailAddress]
	public required string ContactEmail { get; set; }
	[Required]
	[Phone]
	public string? ContactPhone { get; set; }
	public List<int>? Teachers { get; set; }
	public List<int>? Students { get; set; }
	public RegisterSchoolCommand GetCommand()
	{
		return new RegisterSchoolCommand(
			Document,
			Name,
			ContactEmail,
			ContactPhone,
			Teachers,
			Students);
	}
	
	public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
	{
		if (!CnpjValidation.Validate(Document))
		{
			yield return new ValidationResult("Invalid document format.", new[] { nameof(Document) });
		}
	}
}