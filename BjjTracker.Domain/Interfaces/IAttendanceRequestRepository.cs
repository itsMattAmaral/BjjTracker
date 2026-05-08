using BjjTracker.Domain.Entities;

namespace BjjTracker.Domain.Interfaces;

public interface IAttendanceRequestRepository
{
	Task AddAttendanceRequest(AttendanceRequest attendanceRequest, CancellationToken cancellationToken);
	Task <AttendanceRequest?> GetAttendanceRequest(int classId, int studentId, CancellationToken cancellationToken);
	Task<IEnumerable<AttendanceRequest>> GetAllAttendanceRequestsAsync(int page, int pageSize, string? sortBy, bool sortDescending, CancellationToken cancellationToken);
	Task<IEnumerable<AttendanceRequest>> GetAttendanceRequestsByClassId(int classId, CancellationToken cancellationToken);
	Task<int> CountAttendanceRequestsByClassId(int classId, CancellationToken cancellationToken);
	Task<IEnumerable<AttendanceRequest>> GetAttendanceRequestsByStudentId(int studentId, CancellationToken cancellationToken);
	Task<int> CountAttendanceRequestsByStudentId(int studentId, CancellationToken cancellationToken);
	Task<int> CountAttendanceRequests(CancellationToken cancellationToken);
	Task UpdateAttendanceRequest(AttendanceRequest attendanceRequest, CancellationToken cancellationToken);
	
	Task DeleteAttendanceRequest(AttendanceRequest attendanceRequest, CancellationToken cancellationToken);
}