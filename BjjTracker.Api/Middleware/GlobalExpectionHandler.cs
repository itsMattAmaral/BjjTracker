using System.Security.Authentication;
using BjjTracker.Domain.Exceptions.AttendanceRequest;
using BjjTracker.Domain.Exceptions.Class;
using BjjTracker.Domain.Exceptions.School;
using BjjTracker.Domain.Exceptions.Teacher;
using BjjTracker.Domain.Exceptions.User;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace BjjTracker.Api.Middleware;

public class GlobalExpectionHandler(ILogger<GlobalExpectionHandler> logger) : IExceptionHandler
{
	public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
	{
		var (statusCode, title) = exception switch
		{
			UserNotFoundException => (StatusCodes.Status404NotFound, "User not found"),
			SchoolNotFoundException => (StatusCodes.Status404NotFound, "School not found"),
			ClassNotFoundException => (StatusCodes.Status404NotFound, "Class not found"),
			AttendanceRequestNotFoundException => (StatusCodes.Status404NotFound, "Attendance request not found"),
			RoleNotFoundException => (StatusCodes.Status404NotFound, "Role not found"),
			
			IsNotFromTheSameSchoolException => (StatusCodes.Status400BadRequest, "Student and teacher are not from the same school"),
			SchoolExistsException => (StatusCodes.Status400BadRequest, "A school with the same document already exists"),
			TeacherAlreadyOwnsThisSchoolException => (StatusCodes.Status400BadRequest, "Teacher already owns this school"),
			TeacherOwnsAnotherSchoolException => (StatusCodes.Status400BadRequest, "Teacher owns another school"),
			ThisEmailAlreadyExistsException => (StatusCodes.Status400BadRequest, "This email is taken! try another one."),
			UserAlreadyOpenedARequestForThisClassException => (StatusCodes.Status400BadRequest, "Student already opened a request for this class"),
			
			AuthenticationException => (StatusCodes.Status401Unauthorized, "Authentication failed"),
			
			_ => (StatusCodes.Status500InternalServerError, "An unexpected error occurred")
		};

		if (statusCode == StatusCodes.Status500InternalServerError)
		{
			logger.LogError(exception, "An unexpected error occurred: {Message}", exception.Message);
		}
		else
		{
			logger.LogWarning(exception, "Handled exception: {Message}", exception.Message);
		}

		var problemDetails = new ProblemDetails
		{
			Status = statusCode,
			Title = title,
			Detail = exception.Message,
			Instance = httpContext.Request.Path
		};
		
		httpContext.Response.StatusCode = statusCode;
		
		await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);

		return true;
	}
}