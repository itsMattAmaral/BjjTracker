using BjjTracker.Application.Common.Dtos;
using BjjTracker.Application.School.Queries.Dtos;
using BjjTracker.Application.School.Queries.Filters;
using MediatR;

namespace BjjTracker.Application.School.Queries;

public interface ISchoolQueryHandler : 
	IRequestHandler<GetSchoolByDocumentFilter, SchoolDto>,
	IRequestHandler<GetSchoolByIdFilter, SchoolDto>,
	IRequestHandler<SearchSchoolsFilter, PagedResponseDto<SchoolDto>>;