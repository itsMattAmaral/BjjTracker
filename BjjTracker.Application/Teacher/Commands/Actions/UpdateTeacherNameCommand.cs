using MediatR;

namespace BjjTracker.Application.Teacher.Commands.Actions;

public record UpdateTeacherNameCommand(string FirstName, string LastName) : IRequest
{
	public int TeacherId { get; set; }
}