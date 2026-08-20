using StudentManagementSystem.Domain.Entities;

namespace StudentManagementSystem.Domain.Interfaces
{
    public interface IEnrollmentRepository
    {
        Task<IEnumerable<Enrollment>> GetAllWithDetailsAsync(CancellationToken cancellationToken = default);

        Task<Enrollment?> GetByIdAsync(int studentId, int courseId, CancellationToken cancellationToken = default);

        Task<bool> ExistsAsync(int studentId, int courseId, CancellationToken cancellationToken = default);

        Task AddAsync(Enrollment enrollment, CancellationToken cancellationToken = default);

        void Delete(Enrollment enrollment);

        Task SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
