using BjjTracker.Application.Class.Queries.Dtos;
using MediatR;

namespace BjjTracker.Application.Class.Queries.Filters;

public record GetClassByIdFilter(int ClassId) : IRequest<ClassDto>;