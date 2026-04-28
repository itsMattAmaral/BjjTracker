using System.ComponentModel.DataAnnotations;
using BjjTracker.Application.School.Queries.Filters;
using Microsoft.AspNetCore.Mvc;

namespace BjjTracker.Api.Models.School;

public class SearchSchoolsModel
{
	[Required]
	[FromQuery]
	[Range(1, int.MaxValue,  ErrorMessage = "Page must be greater than or equal to 1.")]
	public int Page { get; set; }
	[Required]
	[FromQuery]
	[Range(1, int.MaxValue, ErrorMessage = "PageSize must be greater than or equal to 1.")]
	public int PageSize { get; set; }
	[FromQuery]
	public string? SearchTerm { get; set; }
	[FromQuery]
	public string? SortBy { get; set; }
	[FromQuery]
	public bool SortDescending { get; set; }

	public SearchSchoolsFilter GetFilter()
	{
		return new SearchSchoolsFilter
		{
			Page = Page,
			PageSize = PageSize,
			SearchTerm = SearchTerm,
			SortBy = SortBy,
			SortDescending = SortDescending
		};
	}	
}