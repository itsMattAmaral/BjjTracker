using BjjTracker.Application.Student.Commands.Actions;
using BjjTracker.Domain.Exceptions.School;
using BjjTracker.Domain.Exceptions.User;
using BjjTracker.Domain.Interfaces;

namespace BjjTracker.Application.Student.Commands;

public class StudentCommandHandler(
	IStudentRepository studentRepository,
	ISchoolRepository schoolRepository
	) : IStudentCommandHandler

{
	private readonly IStudentRepository _studentRepository = studentRepository ?? throw new ArgumentNullException(nameof(studentRepository));
	private readonly ISchoolRepository _schoolRepository = schoolRepository ?? throw new ArgumentNullException(nameof(schoolRepository));
	public async Task Handle(UpdateStudentNameCommand request, CancellationToken cancellationToken)
	{
		var student = await _studentRepository.GetByIdAsync(request.StudentId, cancellationToken);
		if (student == null)
		{
			throw new UserNotFoundException(request.StudentId);
		}
		student.UpdateFirstName(request.FirstName);
		student.UpdateLastName(request.LastName);
		
		await _studentRepository.UpdateAsync(student, cancellationToken);
	}

	public async Task Handle(UpdateStudentSchoolIdCommand request, CancellationToken cancellationToken)
	{
		var student = await _studentRepository.GetByIdAsync(request.Id, cancellationToken);
		if (student == null)
		{
			throw new UserNotFoundException(request.Id);
		}
		var school = await _schoolRepository.GetByIdAsync(request.SchoolId, cancellationToken);
		if (school is null) 
			throw new SchoolNotFoundException(request.SchoolId);
		student.UpdateSchool(request.SchoolId);
		school.AddStudent(student);
		
		await _studentRepository.UpdateAsync(student, cancellationToken);
		await _schoolRepository.UpdateAsync(school, cancellationToken);
	}
}