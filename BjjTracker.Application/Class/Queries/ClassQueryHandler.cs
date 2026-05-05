using BjjTracker.Application.AttendanceRequest.Queries.Dtos;
using BjjTracker.Application.Class.Queries.Dtos;
using BjjTracker.Application.Class.Queries.Filters;
using BjjTracker.Application.Common.Dtos;
using BjjTracker.Application.Teacher.Queries.Dtos;
using BjjTracker.Domain.Exceptions.Class;
using BjjTracker.Domain.Interfaces;

namespace BjjTracker.Application.Class.Queries;

public class ClassQueryHandler(IClassRepository classRepository) : IClassQueryHandler
{
	private readonly IClassRepository _classRepository = classRepository ?? throw new ArgumentNullException(nameof(classRepository));
	public async Task<PagedResponseDto<ClassDto>> Handle(SearchClassesFilter request, CancellationToken cancellationToken)
	{
		ArgumentNullException.ThrowIfNull(request);
		var classes = await _classRepository.GetAllClassesAsync(
			request.Page, 
			request.PageSize, 
			request.SearchTerm, 
			request.SortBy, 
			request.SortDescending, 
			cancellationToken);
		
		var classDtos = classes.Select(@class => ToClassDto(@class));
		var totalItems = await _classRepository.CountAsync(request.SearchTerm, cancellationToken);

		var result = new PagedResponseDto<ClassDto>
		{
			Items = classDtos,
			TotalItems = totalItems,
			PageSize = request.PageSize,
			CurrentPage = request.Page,
			TotalPages = (int)Math.Ceiling((double)totalItems / request.PageSize)
		};
		
		return result;
	}

	public async Task<ClassDto?> Handle(GetClassByIdFilter request, CancellationToken cancellationToken)
	{
		var foundedClass = await _classRepository.GetByIdAsync(request.ClassId, cancellationToken);
		if (foundedClass  == null)
			throw new ClassNotFoundException();

		return ToClassDto(foundedClass);
	}

	private static ClassDto ToClassDto(Domain.Entities.Class classEntity)
	{
		return new ClassDto
		{
			Id = classEntity.Id,
			SchoolId = classEntity.SchoolId,
			Teacher = new TeacherDto
			{
				Id = classEntity.Teacher.Id,
				FirstName = classEntity.Teacher.FirstName,
				LastName = classEntity.Teacher.LastName,
				Email = classEntity.Teacher.Email,
				Role = classEntity.Teacher.Role,
				SchoolId = classEntity.Teacher.SchoolId,
				IsSchoolOwner = classEntity.Teacher.IsSchoolOwner,
				SchoolOwnedId = classEntity.Teacher.SchoolOwnedId,
				BeltColor = classEntity.Teacher.BeltColor,
				CreatedAt = classEntity.Teacher.CreatedAt,
				UpdatedAt = classEntity.Teacher.UpdatedAt
			},
			BeginDate = classEntity.BeginDate,
			EndDate = classEntity.EndDate,
			UpdatedAt = classEntity.UpdatedAt,
			AttendanceRequests = classEntity.AttendanceRequests.Count <= 0 ? null 
				: classEntity.AttendanceRequests.Select(
					attendanceRequest => new AttendanceRequestDto
					{
						ClassId = attendanceRequest.ClassId,
						StudentId = attendanceRequest.StudentId,
						Attended = attendanceRequest.Attended
					}).ToList()
		};
	}
}