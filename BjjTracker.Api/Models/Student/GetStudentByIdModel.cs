using System.ComponentModel.DataAnnotations;
using BjjTracker.Application.Student.Queries.Filters;
using Microsoft.AspNetCore.Mvc;

namespace BjjTracker.Api.Models.Student;

public class GetStudentByIdModel
{
	[Required]
	[FromRoute]
	[Range(1, int.MaxValue,  ErrorMessage = "StudentId must be greater than 0")]
	public int StudentId { get; set; }
	public GetStudentByIdFilter GetFilter()
	{
        return new GetStudentByIdFilter
		{
			StudentId = StudentId
		};
	}
}