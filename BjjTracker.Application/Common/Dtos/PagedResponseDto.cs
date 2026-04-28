namespace BjjTracker.Application.Common.Dtos;

public class PagedResponseDto<T>
{
	public IEnumerable<T> Items { get; set; } = [];
	public int TotalItems { get; set; }
	public int PageSize { get; set; }
	public int CurrentPage { get; set; }
	public int TotalPages { get; set; }
	
}