using System.ComponentModel.DataAnnotations;
using BjjTracker.Application.Class.Queries.Filters;
using Microsoft.AspNetCore.Mvc;

namespace BjjTracker.Api.Models.Class;

public class GetClassByIdModel
{
	[Required]
	[FromRoute]
	[MinLength(1)]
	public int ClassId { get; set; }
	
	public GetClassByIdFilter GetFilter()
	{
		return new GetClassByIdFilter(ClassId);
	}
}