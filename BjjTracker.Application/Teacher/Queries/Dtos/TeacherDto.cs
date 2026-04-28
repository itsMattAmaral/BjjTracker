using BjjTracker.Application.Common.Dtos;
using BjjTracker.Domain.Enums;

namespace BjjTracker.Application.Teacher.Queries.Dtos;

public class TeacherDto : BaseUserDto
{
	public int? SchoolId { get; set; }
	public BeltColors BeltColor { get; set; }
	public bool IsSchoolOwner { get; set; }
}