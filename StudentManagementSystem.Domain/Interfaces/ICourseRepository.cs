using StudentManagementSystem.Domain.Entities;

namespace StudentManagementSystem.Domain.Interfaces
{
    public interface ICourseRepository
    {
        Task<IEnumerable<Course>> GetAllAsync(CancellationToken cancellationToken = default);

        Task<Course?> GetByIdAsync(int courseId, CancellationToken cancellationToken = default);

        Task AddAsync(Course course, CancellationToken cancellationToken = default);

        void Update(Course course);

        void Delete(Course course);

        Task<bool> ExistsAsync(int courseId, CancellationToken cancellationToken = default);

        Task SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
