using BjjTracker.Domain.Entities;

namespace BjjTracker.Domain.Interfaces;

public interface IUserRepository
{
	Task<User?> GetByEmailAsync(string email,  CancellationToken cancellationToken);
	Task<bool> ExistsAsync(string email, CancellationToken cancellationToken);
	Task AddAsync(User user, CancellationToken cancellationToken);
	Task UpdateAsync(User user, CancellationToken cancellationToken);
	Task DeleteAsync(User user, CancellationToken cancellationToken);
}