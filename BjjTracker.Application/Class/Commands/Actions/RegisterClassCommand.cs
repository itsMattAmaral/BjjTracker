using MediatR;

namespace BjjTracker.Application.Class.Commands.Actions;

public record RegisterClassCommand(
	int SchoolId,
	int TeacherId,
	DateTime BeginDate,
	DateTime EndDate
	) : IRequest;
