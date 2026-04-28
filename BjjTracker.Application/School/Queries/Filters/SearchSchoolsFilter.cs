using BjjTracker.Application.Common.Dtos;
using BjjTracker.Application.School.Queries.Dtos;
using MediatR;

namespace BjjTracker.Application.School.Queries.Filters;

public record SearchSchoolsFilter(
	int Page = 1,
	int PageSize = 10,
	string? SearchTerm = null,
	string? SortBy = null,
	bool SortDescending = false) : IRequest<PagedResponseDto<SchoolDto>>;