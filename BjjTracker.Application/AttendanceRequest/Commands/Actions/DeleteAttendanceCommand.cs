using MediatR;

namespace BjjTracker.Application.AttendanceRequest.Commands.Actions;

public record DeleteAttendanceCommand(int ClassId, int StudentId) : IRequest;