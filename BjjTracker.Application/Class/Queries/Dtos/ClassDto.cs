using BjjTracker.Application.AttendanceRequest.Queries.Dtos;
using BjjTracker.Application.Teacher.Queries.Dtos;

namespace BjjTracker.Application.Class.Queries.Dtos;

public class ClassDto
{
	public int Id { get; set; }
	public int SchoolId { get; set; }
	public required TeacherDto Teacher { get; set; }
	public DateTime BeginDate { get; set; }
	public DateTime EndDate { get; set; }
	public DateTime UpdatedAt { get; set; }
	public List<AttendanceRequestDto>? AttendanceRequests { get; set; }
}