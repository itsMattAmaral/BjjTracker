using System.ComponentModel.DataAnnotations;
using BjjTracker.Application.AttendanceRequest.Queries.Filters;
using Microsoft.AspNetCore.Mvc;

namespace BjjTracker.Api.Models.AttendanceRequest;

public class SearchAttendanceRequestsModel
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
	public string? SortBy { get; set; }
	[FromQuery]
	public bool SortDescending { get; set; }

	public SearchAttendanceRequestFilter GetFilter()
	{
		return new SearchAttendanceRequestFilter
		{
			Page = Page,
			PageSize = PageSize,
			SortBy = SortBy,
			SortDescending = SortDescending
		};
	}
}