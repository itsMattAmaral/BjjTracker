using BjjTracker.Domain.Entities;

namespace BjjTracker.Domain.Interfaces;

public interface ISchoolRepository
{
	Task<School?> GetByIdAsync(int id, CancellationToken cancellationToken);
	Task<School?> GetByDocumentAsync(string documentId, CancellationToken cancellationToken);
	Task<IEnumerable<School>> GetAllAsync(int page, int pageSize, string? searchTerm = null, string? sortBy = null, bool sortDescending = false, CancellationToken cancellationToken = default);
	Task<bool> SchoolExistsAsync(string document, CancellationToken cancellationToken);
	Task<int> CountAsync(string? searchTerm, CancellationToken cancellationToken);
	Task<bool> SchoolExistsAsync(int id, CancellationToken cancellationToken);
	Task AddAsync(School school, CancellationToken cancellationToken);
	Task UpdateAsync(School school, CancellationToken cancellationToken);
	Task DeleteAsync(int id, CancellationToken cancellationToken);
}