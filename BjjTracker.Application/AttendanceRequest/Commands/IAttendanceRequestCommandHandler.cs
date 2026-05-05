using BjjTracker.Application.AttendanceRequest.Commands.Actions;
using MediatR;

namespace BjjTracker.Application.AttendanceRequest.Commands;

public interface IAttendanceRequestCommandHandler :
	IRequestHandler<RegisterAttendanceCommand>,
	IRequestHandler<ApproveAttendanceCommand>,
	IRequestHandler<DeleteAttendanceCommand>;
