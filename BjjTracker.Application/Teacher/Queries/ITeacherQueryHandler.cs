using BjjTracker.Application.Common.Dtos;
using BjjTracker.Application.Teacher.Queries.Dtos;
using BjjTracker.Application.Teacher.Queries.Filters;
using MediatR;

namespace BjjTracker.Application.Teacher.Queries;

public interface ITeacherQueryHandler :
	IRequestHandler<GetTeacherFilter, TeacherDto?>,
	IRequestHandler<TeacherSearchFilter, PagedResponseDto<TeacherDto>>;