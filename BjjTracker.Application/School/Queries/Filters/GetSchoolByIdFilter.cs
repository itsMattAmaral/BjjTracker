using BjjTracker.Application.School.Queries.Dtos;
using MediatR;

namespace BjjTracker.Application.School.Queries.Filters;

public record GetSchoolByIdFilter(int SchoolId) : IRequest<SchoolDto>;