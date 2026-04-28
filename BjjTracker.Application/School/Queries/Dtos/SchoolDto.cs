using BjjTracker.Application.Class.Queries.Dtos;
using BjjTracker.Application.Student.Queries.Dtos;
using BjjTracker.Application.Teacher.Queries.Dtos;

namespace BjjTracker.Application.School.Queries.Dtos;

public class SchoolDto
{
	public int Id { get; set; }
	public required string Document { get; set; }
	public required string Name { get; set; }
	public required string ContactEmail { get; set; }
	public string? ContactPhone { get; set; }
	public DateTime CreatedAt { get; set; }
	public DateTime UpdatedAt { get; set; }
	public List<StudentDto>? Students { get; set; }
	public List<TeacherDto>? Teachers { get; set; }
	public List<ClassDto>? Classes { get; set; }
}