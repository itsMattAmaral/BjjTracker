using MediatR;

namespace BjjTracker.Application.Teacher.Commands.Actions;

public record GraduateStudentCommand(int StudentId, int TeacherId) : IRequest;