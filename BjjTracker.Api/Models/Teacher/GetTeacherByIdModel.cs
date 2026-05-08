using System.ComponentModel.DataAnnotations;
using BjjTracker.Application.Teacher.Queries.Filters;
using Microsoft.AspNetCore.Mvc;

namespace BjjTracker.Api.Models.Teacher;

public class GetTeacherByIdModel
{
	[Required]
	[Range(1, int.MaxValue,  ErrorMessage = "TeacherId must be greater than 0")]
	public required int TeacherId { get; init; }

	public GetTeacherFilter GetFilter()
	{
		ArgumentOutOfRangeException.ThrowIfNegativeOrZero(TeacherId);
		return new GetTeacherFilter(TeacherId);
	}
}