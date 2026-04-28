using BjjTracker.Application.Common.Dtos;
using BjjTracker.Application.Student.Queries.Dtos;
using MediatR;

namespace BjjTracker.Application.Student.Queries.Filters;

public record SearchStudentsFilter(
	int Page = 1,
	int PageSize = 10,
	string? SearchTerm = null,
	string? SortBy = null,
	bool SortDescending = false) : IRequest<PagedResponseDto<StudentDto>>;