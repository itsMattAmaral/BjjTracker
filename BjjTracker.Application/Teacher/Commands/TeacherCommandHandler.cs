
using BjjTracker.Application.Teacher.Commands.Actions;
using BjjTracker.Domain.Exceptions.School;
using BjjTracker.Domain.Exceptions.User;
using BjjTracker.Domain.Interfaces;

namespace BjjTracker.Application.Teacher.Commands;

public class TeacherCommandHandler(ITeacherRepository teacherRepository, IStudentRepository studentRepository) : ITeacherCommandHandler
{
	private readonly ITeacherRepository  _teacherRepository = teacherRepository ?? throw new ArgumentNullException(nameof(teacherRepository));
	private readonly IStudentRepository _studentRepository = studentRepository ?? throw new ArgumentNullException(nameof(studentRepository));
	public async Task Handle(UpdateTeacherNameCommand request, CancellationToken cancellationToken)
	{
		ArgumentNullException.ThrowIfNull(request);
		var teacher = await _teacherRepository.GetByIdAsync(request.TeacherId,  cancellationToken);
		if (teacher == null) 
			throw new UserNotFoundException(request.TeacherId);
		teacher.UpdateFirstName(request.FirstName);
		teacher.UpdateLastName(request.LastName);
		await _teacherRepository.UpdateAsync(teacher, cancellationToken);
	}

	public async Task Handle(GraduateStudentCommand request, CancellationToken cancellationToken)
	{
		ArgumentNullException.ThrowIfNull(request);
		var student = await _studentRepository.GetByIdAsync(request.StudentId, cancellationToken);
		if (student == null)
			throw new UserNotFoundException(request.StudentId);
		
		var teacher = await _teacherRepository.GetByIdAsync(request.TeacherId,  cancellationToken);
		if (teacher == null) 
			throw new UserNotFoundException(request.TeacherId);
		
		var bothEntitiesHasSchool = teacher.School != null && student.School != null;

		if (bothEntitiesHasSchool)
			throw new IsNotFromTheSameSchoolException();
		
		teacher.GraduateStudent(student);
		await _studentRepository.UpdateAsync(student, cancellationToken);
	}
}