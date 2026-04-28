using MediatR;

namespace BjjTracker.Application.User.Commands.Actions;

public record RegisterUserCommand(string Email, string Password, int Role) : IRequest;