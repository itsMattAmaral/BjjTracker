using BjjTracker.Application.AttendanceRequest.Queries.Dtos;
using MediatR;

namespace BjjTracker.Application.AttendanceRequest.Queries.Filters;

public record GetAttendanceRequestFilter(int StudentId, int ClassId) : IRequest<AttendanceRequestDto>;