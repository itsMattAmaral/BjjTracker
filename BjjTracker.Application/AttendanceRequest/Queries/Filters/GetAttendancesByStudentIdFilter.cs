using BjjTracker.Application.AttendanceRequest.Queries.Dtos;
using MediatR;

namespace BjjTracker.Application.AttendanceRequest.Queries.Filters;

public record GetAttendancesByStudentIdFilter(int StudentId) : IRequest<IEnumerable<AttendanceRequestDto>>;