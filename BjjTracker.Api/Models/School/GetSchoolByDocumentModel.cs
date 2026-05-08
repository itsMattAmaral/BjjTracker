using System.ComponentModel.DataAnnotations;
using BjjTracker.Application.School.Queries.Filters;

namespace BjjTracker.Api.Models.School;

public class GetSchoolByDocumentModel
{
	[Required]
	public required string SchoolDocument { get; init; }
	
	public GetSchoolByDocumentFilter GetFilter()
	{
		ArgumentException.ThrowIfNullOrWhiteSpace(SchoolDocument);
		return new GetSchoolByDocumentFilter(SchoolDocument);
	}
}