using BjjTracker.Domain.Entities;
using BjjTracker.Domain.Interfaces;
using BjjTracker.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace BjjTracker.Infrastructure.Repositories;

public class ClassRepository(BjjTrackerDbContext dbContext) : IClassRepository
{
	private readonly BjjTrackerDbContext _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
	public async Task<Class?> GetByIdAsync(int classId, CancellationToken cancellationToken)
	{
		return await  _dbContext.Classes
			.Include(c => c.Teacher)
			.Include(c => c.AttendanceRequests)
			.FirstOrDefaultAsync(c => c.Id == classId, cancellationToken);
	}

	public async Task<IEnumerable<Class>> GetByTeacherIdAsync(int teacherId, CancellationToken cancellationToken)
	{
		return await _dbContext.Classes
			.Where(c => c.Teacher.Id == teacherId)
			.Include(c => c.Teacher)
			.Include(c => c.AttendanceRequests)
			.ToListAsync(cancellationToken);
	}

	public async Task<IEnumerable<Class>> GetBySchoolIdAsync(int schoolId, CancellationToken cancellationToken)
	{
		return await _dbContext.Classes
			.Where(c => c.SchoolId == schoolId)
			.Include(c => c.Teacher)
			.Include(c => c.AttendanceRequests)
			.ToListAsync(cancellationToken);
	}

	public async Task<IEnumerable<Class>> GetAllClassesAsync(int page, int pageSize, string? searchTerm = null, string? sortBy = null,
		bool sortDescending = false, CancellationToken cancellationToken = default)
	{
		ArgumentNullException.ThrowIfNull(_dbContext);
		ArgumentOutOfRangeException.ThrowIfNegative(page);
		ArgumentOutOfRangeException.ThrowIfNegative(pageSize);
		
		var query = _dbContext.Classes.AsQueryable();

		if (!string.IsNullOrEmpty(searchTerm))
		{
			query = ApplySearchTerm(query, searchTerm);
		}

		if (!string.IsNullOrEmpty(sortBy))
		{
			query = sortDescending
				? query.OrderByDescending(c => EF.Property<object>(c, sortBy))
				: query.OrderBy(c => EF.Property<object>(c, sortBy));
		}
		
		return await query
			.Include(c => c.Teacher)
			.Include(c => c.AttendanceRequests)
			.Skip((page - 1) * pageSize)
			.Take(pageSize)
			.ToListAsync(cancellationToken);
	}

	public async Task<int> CountAsync(string? searchTerm, CancellationToken cancellationToken)
	{
		var query = _dbContext.Classes.AsQueryable();

		if (!string.IsNullOrEmpty(searchTerm))
		{
			query = ApplySearchTerm(query, searchTerm);
		}
		
		return await query.CountAsync(cancellationToken);
	}

	public async Task AddAsync(Class classEntity, CancellationToken cancellationToken)
	{
		await _dbContext.Classes.AddAsync(classEntity, cancellationToken);
		await _dbContext.SaveChangesAsync(cancellationToken);
	}
	
	public async Task UpdateAsync(Class classEntity, CancellationToken cancellationToken)
	{
		_dbContext.Classes.Update(classEntity);
		await _dbContext.SaveChangesAsync(cancellationToken);
	}

	private static IQueryable<Class> ApplySearchTerm(IQueryable<Class> query, string searchTerm)
	{
		return query.Where(c => EF.Functions.Like(c.Teacher.FirstName, $"{searchTerm}%") ||
		                         EF.Functions.Like(c.Teacher.LastName, $"{searchTerm}%") ||
		                         EF.Functions.Like(c.Teacher.Email, $"{searchTerm}%"));
	}
}