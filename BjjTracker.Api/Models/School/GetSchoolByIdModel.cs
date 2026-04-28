using System.ComponentModel.DataAnnotations;
using BjjTracker.Application.School.Queries.Filters;
using Microsoft.AspNetCore.Mvc;

namespace BjjTracker.Api.Models.School;

public class GetSchoolByIdModel
{ 
	[Required]
	[FromRoute]
	public required int SchoolId { get; set; }
	
	public GetSchoolByIdFilter GetFilter()
	{
		return new GetSchoolByIdFilter(SchoolId);
	}
}