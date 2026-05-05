using MediatR;

namespace BjjTracker.Application.AttendanceRequest.Commands.Actions;

public record ApproveAttendanceCommand(int ClassId, int StudentId) : IRequest;