using BjjTracker.Application.Common.Dtos;
using BjjTracker.Application.School.Queries.Dtos;
using BjjTracker.Domain.Enums;

namespace BjjTracker.Application.Teacher.Queries.Dtos;

public class TeacherDto : BaseUserDto
{
	public int? SchoolId { get; set; }
	public BeltColors BeltColor { get; set; }
	public bool IsSchoolOwner { get; set; }
	public int? SchoolOwnedId { get; set; }
	public SchoolDto? SchoolOwned { get; set; }
}