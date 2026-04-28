using BjjTracker.Domain.Entities;
using BjjTracker.Domain.Interfaces;
using BjjTracker.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace BjjTracker.Infrastructure.Repositories;

public class StudentRepository(BjjTrackerDbContext dbContext) : IStudentRepository
{
	private readonly BjjTrackerDbContext _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
	
	public async Task<int>  CountAsync(string? searchTerm = null, CancellationToken cancellationToken = default)
	{
		var query = _dbContext.Users.OfType<Student>().AsQueryable();
		
		if (!string.IsNullOrEmpty(searchTerm))
		{
			query = query.Where(u => EF.Functions.Like(u.Email, $"{searchTerm}%"));
		}
		
		return await query.CountAsync(cancellationToken);
	}
	public async Task<Student?> GetByIdAsync(int id, CancellationToken cancellationToken)
	{
		return await _dbContext.Users.OfType<Student>().FirstOrDefaultAsync(t => t.Id == id, cancellationToken);
	}
	
	public async Task<List<Student>> GetByIdsAsync(List<int> studentIds, CancellationToken cancellationToken)
	{
		return await _dbContext.Users.OfType<Student>().Where(s => studentIds.Contains(s.Id)).ToListAsync(cancellationToken);
	}

	
	public async Task<IEnumerable<Student>> GetAllAsync(int page, int pageSize, string? searchTerm = null, string? sortBy = null, bool sortDescending = false, CancellationToken cancellationToken = default)
	{
		ArgumentNullException.ThrowIfNull(_dbContext);
		ArgumentOutOfRangeException.ThrowIfNegative(page);
		ArgumentOutOfRangeException.ThrowIfNegative(pageSize);
		
		var query = _dbContext.Users.OfType<Student>().AsQueryable();
		query = query.OrderBy(u => u.Id);

		if (!string.IsNullOrEmpty(searchTerm))
		{
			query = query.Where(u => EF.Functions.Like(u.Email, $"{searchTerm}%"));
		}

		if (!string.IsNullOrEmpty(sortBy))
		{
			query = sortDescending
				? query.OrderByDescending(u => EF.Property<object>(u, sortBy))
				: query.OrderBy(u => EF.Property<object>(u, sortBy));
		}
		
		return await  query
			.Skip((page - 1) * pageSize)
			.Take(pageSize)
			.ToListAsync(cancellationToken: cancellationToken);
	}

	public async Task UpdateAsync(Student student, CancellationToken cancellationToken)
	{
		_dbContext.Update(student);
		await _dbContext.SaveChangesAsync(cancellationToken);
	}
}