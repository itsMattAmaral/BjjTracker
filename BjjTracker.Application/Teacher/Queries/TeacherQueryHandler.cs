using BjjTracker.Application.Class.Queries.Dtos;
using BjjTracker.Application.Common.Dtos;
using BjjTracker.Application.School.Queries.Dtos;
using BjjTracker.Application.Student.Queries.Dtos;
using BjjTracker.Application.Teacher.Queries.Dtos;
using BjjTracker.Application.Teacher.Queries.Filters;
using BjjTracker.Domain.Exceptions.User;
using BjjTracker.Domain.Helpers;
using BjjTracker.Domain.Interfaces;

namespace BjjTracker.Application.Teacher.Queries;

public class TeacherQueryHandler(ITeacherRepository teacherRepository) : ITeacherQueryHandler
{
	private readonly ITeacherRepository _teacherRepository = teacherRepository ?? throw new ArgumentNullException(nameof(teacherRepository));

	public async Task<TeacherDto?> Handle(GetTeacherFilter request, CancellationToken cancellationToken)
	{
		var teacher = await _teacherRepository.GetByIdAsync(request.TeacherId, cancellationToken);
		if (teacher == null)
			throw new UserNotFoundException(request.TeacherId);
		return new TeacherDto
		{
			Id = teacher.Id,
			FirstName = teacher.FirstName,
			LastName = teacher.LastName,
			Email = teacher.Email,
			Role = teacher.Role,
			CreatedAt = teacher.CreatedAt,
			UpdatedAt = teacher.UpdatedAt,
			SchoolId = teacher.SchoolId,
			BeltColor = teacher.BeltColor,
			IsSchoolOwner = teacher.IsSchoolOwner,
			SchoolOwnedId = teacher.SchoolOwnedId,
			SchoolOwned = teacher.SchoolOwned != null ? new SchoolDto
			{
				Id = teacher.SchoolOwned.Id,
				Name = teacher.SchoolOwned.Name,
				Document = teacher.SchoolOwned.Document,
				Owners = teacher.SchoolOwned.Owners.Select(owner => new TeacherDto
				{
					Id = owner.Id,
					FirstName = owner.FirstName,
					LastName = owner.LastName,
					Email = owner.Email,
					Role = owner.Role,
					CreatedAt = owner.CreatedAt,
					UpdatedAt = owner.UpdatedAt,
					SchoolId = owner.SchoolId,
					BeltColor = owner.BeltColor,
					IsSchoolOwner = owner.IsSchoolOwner,
					SchoolOwnedId = owner.SchoolOwnedId
				}).ToList(),
				ContactPhone = teacher.SchoolOwned.ContactPhone,
				ContactEmail = teacher.SchoolOwned.ContactEmail,
				CreatedAt = teacher.SchoolOwned.CreatedAt,
				UpdatedAt = teacher.SchoolOwned.UpdatedAt
			} : null
		};
	}

	public async Task<PagedResponseDto<TeacherDto>> Handle(TeacherSearchFilter request, CancellationToken cancellationToken)
	{
		ArgumentNullException.ThrowIfNull(request);
		var teachers = await _teacherRepository.GetAllAsync(
			request.Page,
			request.PageSize,
			request.SearchTerm,
			request.SortBy,
			request.SortDescending,
			cancellationToken);
		
		var teachersDto = teachers.Select(teacher => new TeacherDto
		{
			Id = teacher.Id,
			FirstName = teacher.FirstName,
			LastName = teacher.LastName,
			Email = teacher.Email,
			Role = teacher.Role,
			CreatedAt = teacher.CreatedAt,
			UpdatedAt = teacher.UpdatedAt,
			SchoolId = teacher.SchoolId,
			BeltColor = teacher.BeltColor,
			IsSchoolOwner = teacher.IsSchoolOwner,
			SchoolOwnedId = teacher.SchoolOwnedId,
			SchoolOwned = teacher.SchoolOwned != null ? new SchoolDto
			{
				Id = teacher.SchoolOwned.Id,
				Name = teacher.SchoolOwned.Name,
				Document = teacher.SchoolOwned.Document,
				Owners = teacher.SchoolOwned.Owners.Select(owner => new TeacherDto
				{
					Id = owner.Id,
					FirstName = owner.FirstName,
					LastName = owner.LastName,
					Email = owner.Email,
					Role = owner.Role,
					CreatedAt = owner.CreatedAt,
					UpdatedAt = owner.UpdatedAt,
					SchoolId = owner.SchoolId,
					BeltColor = owner.BeltColor,
					IsSchoolOwner = owner.IsSchoolOwner,
					SchoolOwnedId = owner.SchoolOwnedId
				}).ToList(),
				ContactPhone = teacher.SchoolOwned.ContactPhone,
				ContactEmail = teacher.SchoolOwned.ContactEmail,
				CreatedAt = teacher.SchoolOwned.CreatedAt,
				UpdatedAt = teacher.SchoolOwned.UpdatedAt
			} : null
		});
		
		var totalItems = await _teacherRepository.CountAsync(request.SearchTerm, cancellationToken);
		var result = new PagedResponseDto<TeacherDto>
		{
			Items = teachersDto,
			CurrentPage = request.Page,
			PageSize = request.PageSize,
			TotalItems = totalItems,
			TotalPages = (int)Math.Ceiling((double)totalItems / request.PageSize)
		};
		return result;
	}
}