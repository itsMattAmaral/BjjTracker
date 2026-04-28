using System.ComponentModel.DataAnnotations;
using BjjTracker.Application.Teacher.Queries.Filters;

namespace BjjTracker.Api.Models.Teacher;

public class GetTeacherByIdModel
{
	[Required]
	[Range(1, int.MaxValue,  ErrorMessage = "TeacherId must be greater than 0")]
	public int TeacherId { get; set; }

	public GetTeacherFilter GetFilter()
	{
		return new GetTeacherFilter(TeacherId);
	}
}