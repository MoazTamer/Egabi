using StudentManagementSystem.Domain.Entities;

namespace StudentManagementSystem.Domain.Interfaces
{
    public interface IFacultyRepository
    {
        Task<IEnumerable<Faculty>> GetAllAsync(CancellationToken cancellationToken = default);

        Task<Faculty?> GetByIdAsync(int facultyId, CancellationToken cancellationToken = default);

        Task AddAsync(Faculty faculty, CancellationToken cancellationToken = default);

        void Update(Faculty faculty);

        void Delete(Faculty faculty);

        Task<bool> ExistsAsync(int facultyId, CancellationToken cancellationToken = default);

        Task SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
