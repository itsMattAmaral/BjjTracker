using MediatR;

namespace BjjTracker.Application.School.Commands.Actions;

public record RegisterSchoolCommand(
	string Document,
	string Name,
	string ContactEmail,
	string? ContactPhone,
	List<int> Owners,
	List<int>? Teachers,
	List<int>? Students
	) : IRequest;