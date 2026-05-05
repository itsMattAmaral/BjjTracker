using BjjTracker.Application.AttendanceRequest.Queries.Dtos;
using BjjTracker.Application.AttendanceRequest.Queries.Filters;
using BjjTracker.Application.Common.Dtos;
using MediatR;

namespace BjjTracker.Application.AttendanceRequest.Queries;

public interface IAttendanceRequestQueryHandler :
	IRequestHandler<GetAttendanceRequestFilter, AttendanceRequestDto>,
	IRequestHandler<GetAttendancesByStudentIdFilter, IEnumerable<AttendanceRequestDto>>,
	IRequestHandler<GetAttendancesByClassIdFilter, IEnumerable<AttendanceRequestDto>>,
	IRequestHandler<SearchAttendanceRequestFilter, PagedResponseDto<AttendanceRequestDto>>;