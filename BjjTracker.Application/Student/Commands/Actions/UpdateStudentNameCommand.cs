using MediatR;

namespace BjjTracker.Application.Student.Commands.Actions;

public record UpdateStudentNameCommand(string FirstName, string LastName) : IRequest
{
	public int StudentId { get; init; }
}