using BjjTracker.Application.Common.Dtos;
using BjjTracker.Application.Teacher.Queries.Dtos;
using MediatR;

namespace BjjTracker.Application.Teacher.Queries.Filters;

public record TeacherSearchFilter(
	int Page = 1,
	int PageSize = 10,
	string? SearchTerm = null,
	string? SortBy = null,
	bool SortDescending = false) : IRequest<PagedResponseDto<TeacherDto>>;