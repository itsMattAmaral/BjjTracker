using MediatR;

namespace BjjTracker.Application.Student.Commands.Actions;

public record UpdateStudentSchoolIdCommand(int SchoolId) : IRequest
{
	public int Id { get; init; }
}