using BjjTracker.Application.Common.Dtos;
using BjjTracker.Application.Student.Queries.Dtos;
using BjjTracker.Application.Student.Queries.Filters;
using MediatR;

namespace BjjTracker.Application.Student.Queries;

public interface IStudentQueryHandler :
	IRequestHandler<GetStudentByIdFilter, StudentDto?>,
	IRequestHandler<SearchStudentsFilter, PagedResponseDto<StudentDto>>;