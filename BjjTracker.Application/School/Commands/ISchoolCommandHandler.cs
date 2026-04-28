using BjjTracker.Application.School.Commands.Actions;
using MediatR;

namespace BjjTracker.Application.School.Commands;

public interface ISchoolCommandHandler : IRequestHandler<RegisterSchoolCommand>;
