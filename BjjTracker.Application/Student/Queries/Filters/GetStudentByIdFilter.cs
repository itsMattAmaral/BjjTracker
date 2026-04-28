using BjjTracker.Application.Student.Queries.Dtos;
using MediatR;

namespace BjjTracker.Application.Student.Queries.Filters;

public record GetStudentByIdFilter : IRequest<StudentDto>
{
	public int StudentId { get; set; }
}