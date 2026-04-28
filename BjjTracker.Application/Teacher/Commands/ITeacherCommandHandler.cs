using BjjTracker.Application.Teacher.Commands.Actions;
using MediatR;

namespace BjjTracker.Application.Teacher.Commands;

public interface ITeacherCommandHandler : 
	IRequestHandler<UpdateTeacherNameCommand>,
	IRequestHandler<GraduateStudentCommand>;