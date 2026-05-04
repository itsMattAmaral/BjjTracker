using System.ComponentModel.DataAnnotations;
using BjjTracker.Application.Student.Queries.Filters;
using Microsoft.AspNetCore.Mvc;

namespace BjjTracker.Api.Models.Student;

public class GetStudentByIdModel
{
	[Required]
	[Range(1, int.MaxValue,  ErrorMessage = "StudentId must be greater than 0")]
	public int StudentId { get; init; }
	public GetStudentByIdFilter GetFilter()
	{
		ArgumentOutOfRangeException.ThrowIfNegativeOrZero(StudentId);
        return new GetStudentByIdFilter
		{
			StudentId = StudentId
		};
	}
}