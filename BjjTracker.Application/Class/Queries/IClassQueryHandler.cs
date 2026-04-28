using BjjTracker.Application.Class.Queries.Dtos;
using BjjTracker.Application.Class.Queries.Filters;
using BjjTracker.Application.Common.Dtos;
using MediatR;

namespace BjjTracker.Application.Class.Queries;

public interface IClassQueryHandler 
	: IRequestHandler<SearchClassesFilter, PagedResponseDto<ClassDto>>,
	  IRequestHandler<GetClassByIdFilter, ClassDto?>;