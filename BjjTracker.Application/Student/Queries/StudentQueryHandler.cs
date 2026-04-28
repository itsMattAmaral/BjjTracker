using BjjTracker.Application.Common.Dtos;
using BjjTracker.Application.Student.Queries.Dtos;
using BjjTracker.Application.Student.Queries.Filters;
using BjjTracker.Domain.Exceptions.User;
using BjjTracker.Domain.Helpers;
using BjjTracker.Domain.Interfaces;

namespace BjjTracker.Application.Student.Queries;

public class StudentQueryHandler(IStudentRepository studentRepository) : IStudentQueryHandler
{
	private readonly IStudentRepository _studentRepository = studentRepository ?? throw new ArgumentNullException(nameof(studentRepository));
	public async Task<StudentDto?> Handle(GetStudentByIdFilter request, CancellationToken cancellationToken)
	{
		var foundedStudent = await _studentRepository.GetByIdAsync(request.StudentId, cancellationToken);
		if  (foundedStudent == null)
			throw new UserNotFoundException(request.StudentId);
		
		return new StudentDto
		{
			Id = foundedStudent.Id,
			FirstName = foundedStudent.FirstName,
			LastName = foundedStudent.LastName,
			Email = foundedStudent.Email,
			Role = foundedStudent.Role,
			CreatedAt = foundedStudent.CreatedAt,
			UpdatedAt = foundedStudent.UpdatedAt,
			SchoolId = foundedStudent.SchoolId,
			BeltColor = foundedStudent.BeltColor,
			ClassesAttended = foundedStudent.ClassesAttended
		};
	}
	
	public async Task<PagedResponseDto<StudentDto>> Handle(SearchStudentsFilter request, CancellationToken cancellationToken)
	{
		ArgumentNullException.ThrowIfNull(request);
		var students = await _studentRepository.GetAllAsync(
			request.Page, 
			request.PageSize, 
			request.SearchTerm, 
			request.SortBy, 
			request.SortDescending, 
			cancellationToken);
		
		var studentsDto = students.Select(student => new StudentDto
		{
			Id = student.Id,
			FirstName = student.FirstName,
			LastName = student.LastName,
			Email = student.Email,
			Role = student.Role,
			CreatedAt = student.CreatedAt,
			UpdatedAt = student.UpdatedAt,
			SchoolId = student.SchoolId,
			BeltColor = student.BeltColor,
			ClassesAttended = student.ClassesAttended
		});
		
		var totalItems = await _studentRepository.CountAsync(request.SearchTerm, cancellationToken);
		var result = new PagedResponseDto<StudentDto>
		{
			Items = studentsDto,
			TotalItems = totalItems,
			CurrentPage = request.Page,
			PageSize = request.PageSize,
			TotalPages = (int)Math.Ceiling((double)totalItems / request.PageSize)
		};
		return result;
	}
}