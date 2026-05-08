using MediatR;

namespace BjjTracker.Application.AttendanceRequest.Commands.Actions;

public record RegisterAttendanceCommand(int ClassId, int StudentId) : IRequest;