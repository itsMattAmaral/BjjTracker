using BjjTracker.Application.Student.Commands.Actions;
using MediatR;

namespace BjjTracker.Application.Student.Commands;

public interface IStudentCommandHandler :
	IRequestHandler<UpdateStudentNameCommand>,
	IRequestHandler<UpdateStudentSchoolIdCommand>
{}