using BjjTracker.Application.AttendanceRequest.Queries.Dtos;
using BjjTracker.Application.Common.Dtos;
using MediatR;

namespace BjjTracker.Application.AttendanceRequest.Queries.Filters;

public record SearchAttendanceRequestFilter(	
	int Page = 1,
	int PageSize = 10,
	string? SortBy = null,
	bool SortDescending = false) : IRequest<PagedResponseDto<AttendanceRequestDto>>;