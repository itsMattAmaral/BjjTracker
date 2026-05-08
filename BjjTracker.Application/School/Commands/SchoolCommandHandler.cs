using BjjTracker.Application.School.Commands.Actions;
using BjjTracker.Domain.Exceptions.School;
using BjjTracker.Domain.Exceptions.Teacher;
using BjjTracker.Domain.Exceptions.User;
using BjjTracker.Domain.Interfaces;

namespace BjjTracker.Application.School.Commands;

public class SchoolCommandHandler(ISchoolRepository schoolRepository, ITeacherRepository teacherRepository, IStudentRepository studentRepository) : ISchoolCommandHandler
{
	private readonly ISchoolRepository _schoolRepository = schoolRepository ?? throw new ArgumentException(nameof(schoolRepository));
	private readonly ITeacherRepository _teacherRepository = teacherRepository ?? throw new ArgumentException(nameof(teacherRepository));
	private readonly IStudentRepository _studentRepository = studentRepository ?? throw new ArgumentException(nameof(studentRepository));
	public async Task Handle(RegisterSchoolCommand request, CancellationToken cancellationToken)
	{
		ArgumentNullException.ThrowIfNull(request);
		var schoolExists = await _schoolRepository.SchoolExistsAsync(request.Document, cancellationToken);
		if (schoolExists) throw new SchoolExistsException();
		
		var newSchool = new Domain.Entities.School(
			request.Document,
			request.Name,
			request.ContactEmail,
			request.ContactPhone);

		var filteredOwnerIds = request.Owners.Where(id => id != newSchool.Id && id > 0).ToList();
		var newOwners = await _teacherRepository.GetByIdsAsync(filteredOwnerIds, cancellationToken);
		newSchool.SetOwners(newOwners);
		
		if (request.Teachers != null)
		{
			var filteredTeacherIds = request.Teachers.Where(id => id != newSchool.Id && id > 0).ToList();
			var newTeachers = await _teacherRepository.GetByIdsAsync(filteredTeacherIds, cancellationToken);
			newSchool.SetTeachers(newTeachers);
		}

		if (request.Students != null)
		{
			var filteredStudentIds = request.Students.Where(id => id != newSchool.Id && id > 0).ToList();
			var newStudents = await _studentRepository.GetByIdsAsync(filteredStudentIds, cancellationToken);
			newSchool.SetStudents(newStudents);
		}
		
		await _schoolRepository.AddAsync(newSchool, cancellationToken);
	}

	public async Task Handle(AddOwnerCommand request, CancellationToken cancellationToken)
	{
		ArgumentNullException.ThrowIfNull(request);
		var teacher = await _teacherRepository.GetByIdAsync(request.TeacherId, cancellationToken);
		if (teacher == null)
			throw new UserNotFoundException();

		if (teacher.IsSchoolOwner)
			throw new TeacherOwnsAnotherSchoolException(teacher.Id);
		
		var school = await _schoolRepository.GetByIdAsync(request.SchoolId, cancellationToken);
		if (school == null)
			throw new SchoolNotFoundException();
		
		if (school.Owners.Contains(teacher))
			throw new TeacherAlreadyOwnsThisSchoolException(teacher.Id, school.Id);
		
		school.AddOwner(teacher);
		await _schoolRepository.UpdateAsync(school, cancellationToken);
	}

	public async Task Handle(RemoveOwnerCommand request, CancellationToken cancellationToken)
	{
		ArgumentNullException.ThrowIfNull(request);
		var teacher = await _teacherRepository.GetByIdAsync(request.TeacherId, cancellationToken);
		if (teacher == null)
			throw new UserNotFoundException();
		
		var school = await _schoolRepository.GetByIdAsync(request.SchoolId, cancellationToken);
		if (school == null)
			throw new SchoolNotFoundException();
		
		school.RemoveOwner(teacher);
		await _schoolRepository.UpdateAsync(school, cancellationToken);
	}
}