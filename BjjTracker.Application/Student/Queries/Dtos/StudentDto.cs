using BjjTracker.Application.Common.Dtos;
using BjjTracker.Domain.Enums;

namespace BjjTracker.Application.Student.Queries.Dtos;

public class StudentDto : BaseUserDto
{
	public int? SchoolId { get; set; }
	public BeltColors BeltColor { get; set; }
	public int ClassesAttended { get; set; }
}