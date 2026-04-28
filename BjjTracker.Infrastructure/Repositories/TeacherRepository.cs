using BjjTracker.Domain.Entities;
using BjjTracker.Domain.Interfaces;
using BjjTracker.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace BjjTracker.Infrastructure.Repositories;

public class TeacherRepository(BjjTrackerDbContext dbContext) : ITeacherRepository
{
	private readonly BjjTrackerDbContext _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));

	public async Task<int> CountAsync(string? searchTerm = null, CancellationToken cancellationToken = default)
	{
		var query = _dbContext.Users.OfType<Teacher>().AsQueryable();

		if (!string.IsNullOrEmpty(searchTerm))
		{
			query = query.Where(t => EF.Functions.Like(t.Email, $"{searchTerm}%"));
		}
		
		return await query.CountAsync(cancellationToken);
	}

	public async Task<Teacher?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
	{
		return await _dbContext.Users.OfType<Teacher>().FirstOrDefaultAsync(t => t.Id == id, cancellationToken);
	}

	public async Task<List<Teacher>> GetByIdsAsync(List<int> teacherIds, CancellationToken cancellationToken = default)
	{
		return await _dbContext.Users.OfType<Teacher>().Where(t => teacherIds.Contains(t.Id)).ToListAsync(cancellationToken);
	}

	public async Task<IEnumerable<Teacher>> GetAllAsync(int page, int pageSize, string? searchTerm = null, string? sortBy = null, bool sortDescending = false, CancellationToken cancellationToken = default)
	{
		var query = _dbContext.Users.OfType<Teacher>().AsQueryable();
		query = query.OrderBy(t => t.Id);

		if (!string.IsNullOrEmpty(searchTerm))
		{
			query = query.Where(t => EF.Functions.Like(t.Email, $"{searchTerm}%"));
		}

		if (!string.IsNullOrEmpty(sortBy))
		{
			query = sortDescending
				? query.OrderByDescending(t => EF.Property<object>(t, sortBy))
				: query.OrderBy(t => EF.Property<object>(t, sortBy));
		}

		return await query
			.Skip((page - 1) * pageSize)
			.Take(pageSize)
			.ToListAsync(cancellationToken);
	}
	
	public async Task UpdateAsync(Teacher teacher, CancellationToken cancellationToken = default)
	{
		_dbContext.Users.Update(teacher);
		await _dbContext.SaveChangesAsync(cancellationToken);
	}
}