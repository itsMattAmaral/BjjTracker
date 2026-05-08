using BjjTracker.Domain.Entities;
using BjjTracker.Domain.Interfaces;
using BjjTracker.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace BjjTracker.Infrastructure.Repositories;

public class AttendanceRequestRepository(BjjTrackerDbContext context) : IAttendanceRequestRepository
{
	private readonly BjjTrackerDbContext _dbContext = context ?? throw new ArgumentNullException(nameof(context));
	
	public async Task AddAttendanceRequest(AttendanceRequest attendanceRequest, CancellationToken cancellationToken)
	{
		_dbContext.AttendanceRequests.Add(attendanceRequest);
		await _dbContext.SaveChangesAsync(cancellationToken);
	}

	public async Task<AttendanceRequest?> GetAttendanceRequest(int classId, int studentId, CancellationToken cancellationToken)
	{
		return await _dbContext.AttendanceRequests.FirstOrDefaultAsync(ar => ar.ClassId == classId && ar.StudentId == studentId, cancellationToken);
	}

	public async Task<IEnumerable<AttendanceRequest>> GetAllAttendanceRequestsAsync(int page, int pageSize, string? sortBy, bool sortDescending,
		CancellationToken cancellationToken)
	{
		ArgumentNullException.ThrowIfNull(_dbContext);
		ArgumentOutOfRangeException.ThrowIfNegativeOrZero(page);
		ArgumentOutOfRangeException.ThrowIfNegativeOrZero(pageSize);
		
		var query = _dbContext.AttendanceRequests.AsQueryable();

		if (!string.IsNullOrWhiteSpace(sortBy))
		{
			query = sortDescending 
				? query.OrderByDescending(ar => EF.Property<object>(ar, sortBy ?? ""))
				: query.OrderBy(ar => EF.Property<object>(ar, sortBy ?? ""));
		}
		
		return await query
			.Include(ar => ar.Class)
			.Include(ar => ar.Student)
			.Skip((page - 1) * pageSize)
			.Take(pageSize)
			.ToListAsync(cancellationToken);
	}

	public async Task<IEnumerable<AttendanceRequest>> GetAttendanceRequestsByClassId(int classId, CancellationToken cancellationToken)
	{
		return await _dbContext.AttendanceRequests.Where(ar => ar.ClassId == classId).ToListAsync(cancellationToken);
	}

	public async Task<int> CountAttendanceRequestsByClassId(int classId, CancellationToken cancellationToken)
	{
		return  await _dbContext.AttendanceRequests.CountAsync(ar => ar.ClassId == classId, cancellationToken);
	}

	public async Task<IEnumerable<AttendanceRequest>> GetAttendanceRequestsByStudentId(int studentId, CancellationToken cancellationToken)
	{
		return await _dbContext.AttendanceRequests.Where(ar => ar.StudentId == studentId).ToListAsync(cancellationToken);
	}

	public async Task<int> CountAttendanceRequestsByStudentId(int studentId, CancellationToken cancellationToken)
	{
		return await _dbContext.AttendanceRequests.CountAsync(ar => ar.StudentId == studentId, cancellationToken);
	}

	public async Task<int> CountAttendanceRequests(CancellationToken cancellationToken)
	{
		return await _dbContext.AttendanceRequests.CountAsync(cancellationToken);
	}

	public async Task UpdateAttendanceRequest(AttendanceRequest attendanceRequest, CancellationToken cancellationToken)
	{
		_dbContext.AttendanceRequests.Update(attendanceRequest);
		await _dbContext.SaveChangesAsync(cancellationToken);
	}

	public async Task DeleteAttendanceRequest(AttendanceRequest attendanceRequest, CancellationToken cancellationToken)
	{
		_dbContext.AttendanceRequests.Remove(attendanceRequest);
		await _dbContext.SaveChangesAsync(cancellationToken);
	}
}