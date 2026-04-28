using BjjTracker.Application.Class.Queries.Dtos;
using BjjTracker.Application.Common.Dtos;
using MediatR;

namespace BjjTracker.Application.Class.Queries.Filters;

public record SearchClassesFilter(
	int Page = 1,
	int PageSize = 10,
	string? SearchTerm = null,
	string? SortBy = null,
	bool SortDescending = false) : IRequest<PagedResponseDto<ClassDto>>;