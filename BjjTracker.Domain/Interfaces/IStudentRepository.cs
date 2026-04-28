using BjjTracker.Domain.Entities;

namespace BjjTracker.Domain.Interfaces;

public interface IStudentRepository
{
	Task <Student?> GetByIdAsync(int id, CancellationToken cancellationToken);
	Task<List<Student>> GetByIdsAsync(List<int> studentIds, CancellationToken cancellationToken);
	Task<IEnumerable<Student>> GetAllAsync(int page, int pageSize, string? searchTerm = null, string? sortBy = null, bool sortDescending = false, CancellationToken cancellationToken = default);
	Task<int> CountAsync(string? searchTerm, CancellationToken cancellationToken);
	Task UpdateAsync(Student user, CancellationToken cancellationToken);
}