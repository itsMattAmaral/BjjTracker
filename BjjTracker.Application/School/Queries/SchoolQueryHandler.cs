using BjjTracker.Application.AttendanceRequest.Queries.Dtos;
using BjjTracker.Application.Class.Queries.Dtos;
using BjjTracker.Application.Common.Dtos;
using BjjTracker.Application.School.Queries.Dtos;
using BjjTracker.Application.School.Queries.Filters;
using BjjTracker.Application.Student.Queries.Dtos;
using BjjTracker.Application.Teacher.Queries.Dtos;
using BjjTracker.Domain.Exceptions.School;
using BjjTracker.Domain.Interfaces;

namespace BjjTracker.Application.School.Queries;

public class SchoolQueryHandler(ISchoolRepository schoolRepository) : ISchoolQueryHandler
{
	private readonly ISchoolRepository _schoolRepository = schoolRepository ?? throw new ArgumentNullException(nameof(schoolRepository));
	public async Task<SchoolDto> Handle(GetSchoolByDocumentFilter request, CancellationToken cancellationToken)
	{
		var school = await _schoolRepository.GetByDocumentAsync(request.Document, cancellationToken);
		if (school == null)
			throw new SchoolNotFoundException();
		
		return ToSchoolDto(school);
	}

	public async Task<SchoolDto> Handle(GetSchoolByIdFilter request, CancellationToken cancellationToken)
	{
		var school = await _schoolRepository.GetByIdAsync(request.SchoolId, cancellationToken);
		if (school == null)
			throw new SchoolNotFoundException();
		
		return ToSchoolDto(school);
	}

	public async Task<PagedResponseDto<SchoolDto>> Handle(SearchSchoolsFilter request, CancellationToken cancellationToken)
	{
		ArgumentNullException.ThrowIfNull(request);
		var schools = await _schoolRepository.GetAllAsync(
			request.Page, 
			request.PageSize, 
			request.SearchTerm, 
			request.SortBy, 
			request.SortDescending, 
			cancellationToken);
		
		var schoolDtos = schools.Select(school => ToSchoolDto(school));
		var totalItems = await _schoolRepository.CountAsync(request.SearchTerm, cancellationToken);
		
		var result = new PagedResponseDto<SchoolDto>
		{
			Items = schoolDtos,
			TotalItems = totalItems,
			CurrentPage = request.Page,
			PageSize = request.PageSize,
			TotalPages = (int)Math.Ceiling((double)totalItems / request.PageSize)
		};
		return result;
	}

	private static SchoolDto ToSchoolDto(Domain.Entities.School school)
	{
		var studentsDto = school.Students.Select(
			student => new StudentDto
			{
				Id = student.Id,
				FirstName = student.FirstName, 
				LastName = student.LastName,
				Email = student.Email,
				Role = student.Role,
				SchoolId = student.SchoolId,
				BeltColor = student.BeltColor,
				ClassesAttended = student.ClassesAttended,
				UpdatedAt = student.UpdatedAt
				
			}).ToList();

		var teachersDto = school.Teachers.Select(
			teacher => new TeacherDto
			{
				Id = teacher.Id,
				IsSchoolOwner = teacher.IsSchoolOwner,
				FirstName = teacher.FirstName, 
				LastName = teacher.LastName,
				Email = teacher.Email,
				Role = teacher.Role,
				SchoolId = teacher.SchoolId,
				UpdatedAt = teacher.UpdatedAt
			}
			).ToList();

		var classesDto = school.Classes.Select(
			schoolClass => new ClassDto
			{
				Id = schoolClass.Id,
				SchoolId = schoolClass.SchoolId,
				BeginDate = schoolClass.BeginDate,
				EndDate = schoolClass.EndDate,
				UpdatedAt = schoolClass.UpdatedAt,
				Teacher = new TeacherDto
				{
					Id = schoolClass.Teacher.Id,
					FirstName = schoolClass.Teacher.FirstName,
					LastName = schoolClass.Teacher.LastName,
					Email = schoolClass.Teacher.Email,
					Role = schoolClass.Teacher.Role,
					SchoolId = schoolClass.Teacher.SchoolId,
					CreatedAt = schoolClass.CreatedAt,
					UpdatedAt = schoolClass.UpdatedAt
				},
				AttendanceRequests = schoolClass.AttendanceRequests.Count <= 0 ? null 
					: schoolClass.AttendanceRequests.Select(
						attendanceRequest => new AttendanceRequestDto
					{
						ClassId = attendanceRequest.ClassId,
						StudentId = attendanceRequest.StudentId,
						Attended = attendanceRequest.Attended
					}
				).ToList()
			}).ToList();
		
		return new SchoolDto
		{
			Id = school.Id,
			Name = school.Name,
			Document = school.Document,
			ContactEmail = school.ContactEmail,
			ContactPhone = school.ContactPhone,
			CreatedAt = school.CreatedAt,
			UpdatedAt = school.UpdatedAt,
			Students = studentsDto,
			Teachers = teachersDto,
			Classes = classesDto
		};
	}
}