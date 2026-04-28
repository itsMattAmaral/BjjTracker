using System.ComponentModel.DataAnnotations;
using BjjTracker.Application.Student.Queries.Filters;
using Microsoft.AspNetCore.Mvc;

namespace BjjTracker.Api.Models.Student;

public class SearchStudentsModel(int page, int pageSize, string? searchTerm, string? sortBy, bool sortDescending)
{
	[Required]
	[FromQuery]
	[Range(1, int.MaxValue,  ErrorMessage = "Page must be greater than or equal to 1.")]
	public int Page { get; set; } = page;
	[Required]
	[FromQuery]
	[Range(1, int.MaxValue, ErrorMessage = "PageSize must be greater than or equal to 1.")]
	public int PageSize { get; set; } = pageSize;
	[FromQuery]
	public string? SearchTerm { get; set; } = searchTerm;
	[FromQuery]
	public string? SortBy { get; set; } = sortBy;
	[FromQuery]
	public bool SortDescending { get; set; } = sortDescending;
	
	public SearchStudentsFilter GetFilter()
	{
		return new SearchStudentsFilter
		{
			Page = Page,
			PageSize = PageSize,
			SearchTerm = SearchTerm,
			SortBy = SortBy,
			SortDescending = SortDescending
		};
	}
}