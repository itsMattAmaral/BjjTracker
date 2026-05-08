using System.ComponentModel.DataAnnotations;
using BjjTracker.Application.School.Queries.Filters;

namespace BjjTracker.Api.Models.School;

public class GetSchoolByIdModel
{ 
	[Required]
	public required int SchoolId { get; init; }
	
	public GetSchoolByIdFilter GetFilter()
	{
		ArgumentOutOfRangeException.ThrowIfNegative(SchoolId);
		return new GetSchoolByIdFilter(SchoolId);
	}
}