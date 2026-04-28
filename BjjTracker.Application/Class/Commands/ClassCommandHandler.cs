using BjjTracker.Application.Class.Commands.Actions;
using BjjTracker.Domain.Exceptions.School;
using BjjTracker.Domain.Interfaces;

namespace BjjTracker.Application.Class.Commands;

public class ClassCommandHandler(IClassRepository classRepository, ITeacherRepository teacherRepository, ISchoolRepository schoolRepository) : IClassCommandHandler
{
	private readonly IClassRepository _classRepository = classRepository ?? throw new ArgumentNullException(nameof(classRepository));
	private readonly ITeacherRepository _teacherRepository = teacherRepository ?? throw new ArgumentNullException(nameof(teacherRepository));
	private readonly ISchoolRepository _schoolRepository = schoolRepository ?? throw new ArgumentNullException(nameof(schoolRepository));
	
	public async Task Handle(RegisterClassCommand request, CancellationToken cancellationToken)
	{
		ArgumentNullException.ThrowIfNull(request);
		
		var schoolExists = await _schoolRepository.SchoolExistsAsync(request.SchoolId, cancellationToken);
		if (!schoolExists)
			throw new SchoolNotFoundException(request.SchoolId);
		
		var teacher = await _teacherRepository.GetByIdAsync(request.TeacherId, cancellationToken);
		if (teacher == null)
		{
			throw new InvalidOperationException($"Teacher with ID {request.TeacherId} not found.");
		}
		
		var begin = request.BeginDate.Kind == DateTimeKind.Utc
			? request.BeginDate
			: request.BeginDate.ToUniversalTime();

		var end = request.EndDate.Kind == DateTimeKind.Utc
			? request.EndDate
			: request.EndDate.ToUniversalTime();
		
		var newClass = new Domain.Entities.Class
		{
			SchoolId = request.SchoolId,
			Teacher = teacher,
			BeginDate = begin,
			EndDate = end
		};
		await _classRepository.AddAsync(newClass, cancellationToken);
	}
}