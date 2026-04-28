using BjjTracker.Application.School.Commands.Actions;
using BjjTracker.Domain.Exceptions.School;
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
}