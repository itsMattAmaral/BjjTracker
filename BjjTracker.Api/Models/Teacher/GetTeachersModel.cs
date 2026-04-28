using System.ComponentModel.DataAnnotations;
using BjjTracker.Application.Teacher.Queries.Filters;

namespace BjjTracker.Api.Models.Teacher;

public class GetTeachersModel(int page, int pageSize, string? searchTerm, string? sortBy, bool sortDescending)
{
	[Required]
	[Range(1, int.MaxValue,  ErrorMessage = "Page must be greater than or equal to 1.")]
	public int Page { get; set; } = page;
	[Required]
	[Range(1, int.MaxValue, ErrorMessage = "PageSize must be greater than or equal to 1.")]
	public int PageSize { get; set; } = pageSize;
	public string? SearchTerm { get; set; } = searchTerm;
	public string? SortBy { get; set; } = sortBy;
	public bool SortDescending { get; set; } = sortDescending;

	public TeacherSearchFilter GetFilter()
	{
		return new TeacherSearchFilter
		{
			Page = Page,
			PageSize = PageSize,
			SearchTerm = SearchTerm,
			SortBy = SortBy,
			SortDescending = SortDescending
		};
	}
}