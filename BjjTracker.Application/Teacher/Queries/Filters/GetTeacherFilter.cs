using BjjTracker.Application.Teacher.Queries.Dtos;
using MediatR;

namespace BjjTracker.Application.Teacher.Queries.Filters;

public record GetTeacherFilter(int TeacherId) : IRequest<TeacherDto?>;