using BjjTracker.Domain.Entities;
using BjjTracker.Domain.Interfaces;
using BjjTracker.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace BjjTracker.Infrastructure.Repositories;

public class UserRepository(BjjTrackerDbContext dbContext) : IUserRepository
{
	private readonly BjjTrackerDbContext _dbContext = dbContext ??  throw new ArgumentNullException(nameof(dbContext));

	public async Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken)
	{
		return await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == email,  cancellationToken);
	}

	public async Task<bool> ExistsAsync(string email, CancellationToken cancellationToken)
	{
		return await _dbContext.Users.AnyAsync(u => u.Email == email, cancellationToken);
	}

	public async Task AddAsync(User user, CancellationToken cancellationToken)
	{
		await _dbContext.Users.AddAsync(user, cancellationToken);
		await _dbContext.SaveChangesAsync(cancellationToken);
	}

	public async Task UpdateAsync(User user, CancellationToken cancellationToken)
	{
		_dbContext.Users.Update(user);
		await _dbContext.SaveChangesAsync(cancellationToken);
	}

	public async Task DeleteAsync(User user, CancellationToken cancellationToken)
	{
		_dbContext.Users.Remove(user);
		await _dbContext.SaveChangesAsync(cancellationToken);
	}
}