using MediatR;

namespace BjjTracker.Application.School.Commands.Actions;

public record RemoveOwnerCommand(int TeacherId, int SchoolId) : IRequest;