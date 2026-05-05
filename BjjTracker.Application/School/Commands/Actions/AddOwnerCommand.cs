using MediatR;

namespace BjjTracker.Application.School.Commands.Actions;

public record AddOwnerCommand(int TeacherId, int SchoolId) : IRequest;