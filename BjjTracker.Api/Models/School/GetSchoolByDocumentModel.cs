using System.ComponentModel.DataAnnotations;
using BjjTracker.Application.School.Queries.Filters;
using Microsoft.AspNetCore.Mvc;

namespace BjjTracker.Api.Models.School;

public class GetSchoolByDocumentModel
{
	[Required]
	[FromRoute]
	public required string SchoolDocument { get; set; }
	
	public GetSchoolByDocumentFilter GetFilter()
	{
		return new GetSchoolByDocumentFilter(SchoolDocument);
	}
}