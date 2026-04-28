using System.ComponentModel.DataAnnotations;
using BjjTracker.Application.Student.Queries.Filters;

namespace BjjTracker.Api.Models.Student;

public class GetStudentByIdModel
{
	[Required]
	[Range(1, int.MaxValue,  ErrorMessage = "StudentId must be greater than 0")]
	public int Id { get; set; }
	public GetStudentByIdFilter GetFilter()
	{
        return new GetStudentByIdFilter
		{
			StudentId = Id
		};
	}
}