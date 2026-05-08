using BjjTracker.Application.Common.Dtos;
using MediatR;

namespace BjjTracker.Application.User.Commands.Actions;

public record LoginUserCommand(string Email, string Password) : IRequest<LoginDto>;