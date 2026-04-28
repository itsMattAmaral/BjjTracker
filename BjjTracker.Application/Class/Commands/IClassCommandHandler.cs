using BjjTracker.Application.Class.Commands.Actions;
using MediatR;

namespace BjjTracker.Application.Class.Commands;

public interface IClassCommandHandler 
	: IRequestHandler<RegisterClassCommand>;