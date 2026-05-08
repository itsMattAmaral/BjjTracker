using BjjTracker.Application.AttendanceRequest.Queries.Dtos;
using BjjTracker.Application.AttendanceRequest.Queries.Filters;
using BjjTracker.Application.Common.Dtos;
using BjjTracker.Domain.Exceptions.AttendanceRequest;
using BjjTracker.Domain.Interfaces;

namespace BjjTracker.Application.AttendanceRequest.Queries;

public class AttendanceRequestQueryHandler(IAttendanceRequestRepository attendanceRequestRepository) : IAttendanceRequestQueryHandler
{
	private readonly IAttendanceRequestRepository _attendanceRequestRepository = attendanceRequestRepository ?? throw new ArgumentNullException(nameof(attendanceRequestRepository));
	
	public async Task<AttendanceRequestDto> Handle(GetAttendanceRequestFilter request, CancellationToken cancellationToken)
	{
		ArgumentNullException.ThrowIfNull(request);
		var attendanceRequest = await _attendanceRequestRepository.GetAttendanceRequest(request.StudentId, request.ClassId, cancellationToken);
		if (attendanceRequest == null)
			throw new AttendanceRequestNotFoundException();

		return ToAttendanceRequestDto(attendanceRequest);
	}

	public async Task<IEnumerable<AttendanceRequestDto>> Handle(GetAttendancesByStudentIdFilter request, CancellationToken cancellationToken)
	{
		ArgumentNullException.ThrowIfNull(request);
		var attendanceRequests = await _attendanceRequestRepository.GetAttendanceRequestsByStudentId(request.StudentId, cancellationToken);
		return attendanceRequests.Select(ToAttendanceRequestDto);
	}

	public async Task<IEnumerable<AttendanceRequestDto>> Handle(GetAttendancesByClassIdFilter request, CancellationToken cancellationToken)
	{
		ArgumentNullException.ThrowIfNull(request);
		var attendanceRequests = await _attendanceRequestRepository.GetAttendanceRequestsByClassId(request.ClassId, cancellationToken);
		return attendanceRequests.Select(ToAttendanceRequestDto);
		
	}

	public async Task<PagedResponseDto<AttendanceRequestDto>> Handle(SearchAttendanceRequestFilter request, CancellationToken cancellationToken)
	{
		ArgumentNullException.ThrowIfNull(request);
		var attendanceRequests = await _attendanceRequestRepository.GetAllAttendanceRequestsAsync(
			request.Page,
			request.PageSize,
			request.SortBy, 
			request.SortDescending, 
			cancellationToken);

		var attendanceRequestDtos = attendanceRequests.Select(ar => ToAttendanceRequestDto(ar));
		var totalItems = await _attendanceRequestRepository.CountAttendanceRequests(cancellationToken);
		
		return new PagedResponseDto<AttendanceRequestDto>
		{
			Items = attendanceRequestDtos,
			TotalItems = totalItems,
			PageSize = request.PageSize,
			CurrentPage = request.Page,
			TotalPages = (int)Math.Ceiling((double)totalItems / request.PageSize)
		};
	}

	private static AttendanceRequestDto ToAttendanceRequestDto(Domain.Entities.AttendanceRequest attendanceRequest)
	{
		return new AttendanceRequestDto
		{
			ClassId = attendanceRequest.ClassId,
			StudentId = attendanceRequest.StudentId,
			Attended = attendanceRequest.Attended
		};
	}
}