using BjjTracker.Application.AttendanceRequest.Commands.Actions;
using BjjTracker.Domain.Exceptions.AttendanceRequest;
using BjjTracker.Domain.Exceptions.Class;
using BjjTracker.Domain.Exceptions.User;
using BjjTracker.Domain.Interfaces;

namespace BjjTracker.Application.AttendanceRequest.Commands;

public class AttendanceRequestCommandHandler(IAttendanceRequestRepository attendanceRequestRepository, IStudentRepository studentRepository, IClassRepository classRepository) : IAttendanceRequestCommandHandler
{
	private readonly IAttendanceRequestRepository _attendanceRequestRepository = attendanceRequestRepository ?? throw new ArgumentNullException(nameof(attendanceRequestRepository));
	private readonly IStudentRepository  _studentRepository = studentRepository ?? throw new ArgumentNullException(nameof(studentRepository));
	private readonly IClassRepository _classRepository = classRepository ?? throw new ArgumentNullException(nameof(classRepository));
	
	public async Task Handle(RegisterAttendanceCommand request, CancellationToken cancellationToken)
	{
		ArgumentNullException.ThrowIfNull(request);
		var student = await _studentRepository.GetByIdAsync(request.StudentId, cancellationToken);
		if (student is null) throw new UserNotFoundException($"Student with id {request.StudentId} not found");
		
		var @class = await _classRepository.GetByIdAsync(request.ClassId, cancellationToken);
		if (@class is null) throw new ClassNotFoundException();
		
		var existingRequest = await _attendanceRequestRepository.GetAttendanceRequest(request.ClassId, request.StudentId, cancellationToken);
		if (existingRequest != null) throw new UserAlreadyOpenedARequestForThisClassException();
		
		var newAttendanceRequest = new Domain.Entities.AttendanceRequest
		{
			ClassId = request.ClassId,
			StudentId = request.StudentId,
			Attended = false
		};
		
		await _attendanceRequestRepository.AddAttendanceRequest(newAttendanceRequest, cancellationToken);
		@class.AddAttendanceRequest(newAttendanceRequest);
		await _classRepository.UpdateAsync(@class, cancellationToken);
	}

	public async Task Handle(ApproveAttendanceCommand request, CancellationToken cancellationToken)
	{
		ArgumentNullException.ThrowIfNull(request);
		var student = await _studentRepository.GetByIdAsync(request.StudentId, cancellationToken);
		if (student == null) throw new UserNotFoundException($"Student with id {request.StudentId} not found");
		
		var attendanceRequest =  await _attendanceRequestRepository.GetAttendanceRequest(request.ClassId, request.StudentId, cancellationToken);
		if (attendanceRequest is null) throw new AttendanceRequestNotFoundException();
		
		attendanceRequest.ApproveAttendedRequest();
		await _attendanceRequestRepository.UpdateAttendanceRequest(attendanceRequest, cancellationToken);
		
		student.ClassesAttended += 1;
		await _studentRepository.UpdateAsync(student, cancellationToken);
	}

	public async Task Handle(DeleteAttendanceCommand request, CancellationToken cancellationToken)
	{
		ArgumentNullException.ThrowIfNull(request);
		
		var attendanceRequest = await _attendanceRequestRepository.GetAttendanceRequest(request.ClassId, request.StudentId, cancellationToken);
		if (attendanceRequest == null) throw new AttendanceRequestNotFoundException();
		
		await _attendanceRequestRepository.DeleteAttendanceRequest(attendanceRequest, cancellationToken);
	}
}