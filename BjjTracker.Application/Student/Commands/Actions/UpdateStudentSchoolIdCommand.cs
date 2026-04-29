using MediatR;

namespace BjjTracker.Application.Student.Commands.Actions;

public record UpdateStudentSchoolIdCommand(int SchoolId) : IRequest
{
	public int StudentId { get; init; }
}