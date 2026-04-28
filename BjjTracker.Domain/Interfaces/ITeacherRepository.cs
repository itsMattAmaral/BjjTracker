using BjjTracker.Domain.Entities;

namespace BjjTracker.Domain.Interfaces;

public interface ITeacherRepository
{
	Task <Teacher?> GetByIdAsync(int id, CancellationToken cancellationToken);
	Task <List<Teacher>> GetByIdsAsync(List<int> teacherIds, CancellationToken cancellationToken);
	Task<IEnumerable<Teacher>> GetAllAsync(int page, int pageSize, string? searchTerm = null, string? sortBy = null, bool sortDescending = false, CancellationToken cancellationToken = default);
	Task<int> CountAsync(string? searchTerm, CancellationToken cancellationToken);
	Task UpdateAsync(Teacher user, CancellationToken cancellationToken);
}