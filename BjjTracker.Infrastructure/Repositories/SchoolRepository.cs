using BjjTracker.Domain.Entities;
using BjjTracker.Domain.Interfaces;
using BjjTracker.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace BjjTracker.Infrastructure.Repositories;

public class SchoolRepository(BjjTrackerDbContext bjjContext) : ISchoolRepository
{
	private readonly BjjTrackerDbContext _dbContext = bjjContext ?? throw new ArgumentNullException(nameof(bjjContext));

	public async Task<School?> GetByIdAsync(int id, CancellationToken cancellationToken)
	{
		return await _dbContext.Schools
			.Include(s => s.Teachers)
			.Include(s => s.Students)
			.Include(s => s.Classes).ThenInclude(c => c.Teacher)
			.Include(s => s.Classes).ThenInclude(c => c.AttendanceRequests)
			.FirstOrDefaultAsync(s => s.Id == id, cancellationToken);
	}

	public async Task<School?> GetByDocumentAsync(string document, CancellationToken cancellationToken)
	{
		return await _dbContext.Schools
			.Include(s => s.Teachers)
			.Include(s => s.Students)
			.Include(s => s.Classes).ThenInclude(c => c.Teacher)
			.Include(s => s.Classes).ThenInclude(c => c.AttendanceRequests)
			.FirstOrDefaultAsync(s => s.Document == document, cancellationToken);
	}

	public async Task<int> CountAsync(string? searchTerm = null, CancellationToken cancellationToken = default)
	{
		var  query = _dbContext.Schools.AsQueryable();

		if (!string.IsNullOrEmpty(searchTerm))
		{
			query = query.Where(s => s.Name.Contains(searchTerm));
		}
		
		return await query.CountAsync(cancellationToken);
	}
	
	public async Task<bool> SchoolExistsAsync(int id, CancellationToken cancellationToken)
	{
		return await _dbContext.Schools.AnyAsync(s => s.Id == id, cancellationToken);
	}
	public async Task<IEnumerable<School>> GetAllAsync(int page, int pageSize, string? searchTerm = null, string? sortBy = null, bool sortDescending = false, CancellationToken cancellationToken = default)
	{ 
		ArgumentNullException.ThrowIfNull(_dbContext);
		ArgumentOutOfRangeException.ThrowIfNegative(page);
		ArgumentOutOfRangeException.ThrowIfNegative(pageSize);
		
		var query = _dbContext.Schools.AsQueryable();
		if (!string.IsNullOrEmpty(searchTerm))
		{
			query = query.Where(s => s.Name.Contains(searchTerm));
		}

		if (!string.IsNullOrEmpty(sortBy))
		{
			query = sortDescending ? query.OrderByDescending(s => EF.Property<object>(s, sortBy))
				: query.OrderBy(s => EF.Property<object>(s, sortBy));
		}
		
		return await  query
			.Include(s => s.Teachers)
			.Include(s => s.Students)
			.Include(s => s.Classes).ThenInclude(c => c.Teacher)
			.Include(s => s.Classes).ThenInclude(c => c.AttendanceRequests)
			.Skip((page - 1) * pageSize)
			.Take(pageSize)
			.ToListAsync(cancellationToken: cancellationToken);
	}

	public async Task<bool> SchoolExistsAsync(string document, CancellationToken cancellationToken)
	{
		return await _dbContext.Schools.AnyAsync(s => s.Document == document, cancellationToken);
	}

	public async Task AddAsync(School school, CancellationToken cancellationToken)
	{
		await _dbContext.Schools.AddAsync(school, cancellationToken);
		await _dbContext.SaveChangesAsync(cancellationToken);
	}

	public async Task UpdateAsync(School school, CancellationToken cancellationToken)
	{
		_dbContext.Schools.Update(school);
		await _dbContext.SaveChangesAsync(cancellationToken);
	}

	public async Task DeleteAsync(int id, CancellationToken cancellationToken)
	{
		var school = await GetByIdAsync(id, cancellationToken);
		if (school != null)
		{
			_dbContext.Schools.Remove(school);
			await _dbContext.SaveChangesAsync(cancellationToken);
		}
	}
}