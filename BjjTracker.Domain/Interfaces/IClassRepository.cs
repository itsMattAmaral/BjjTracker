using BjjTracker.Domain.Entities;

namespace BjjTracker.Domain.Interfaces;

public interface IClassRepository
{
	Task<Class?> GetByIdAsync(int classId, CancellationToken cancellationToken);
	Task<IEnumerable<Class>> GetByTeacherIdAsync(int teacherId, CancellationToken cancellationToken);
	Task<IEnumerable<Class>> GetBySchoolIdAsync(int schoolId, CancellationToken cancellationToken);
	Task<IEnumerable<Class>> GetAllClassesAsync(int page, int pageSize, string? searchTerm = null, string? sortBy = null, bool sortDescending = false, CancellationToken cancellationToken = default);
	Task<int> CountAsync(string? searchTerm, CancellationToken cancellationToken);
	Task AddAsync(Class classEntity, CancellationToken cancellationToken);
	Task UpdateAsync(Class classEntity, CancellationToken cancellationToken);
}