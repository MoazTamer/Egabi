using StudentManagementSystem.Domain.Entities;

namespace StudentManagementSystem.Domain.Interfaces
{
    public interface IStudentRepository
    {
        IQueryable<Student> GetQueryable();

        Task<Student?> GetByIdAsync(int studentId, CancellationToken cancellationToken = default);

        Task<Student?> GetByIdWithDetailsAsync(int studentId, CancellationToken cancellationToken = default);

        Task AddAsync(Student student, CancellationToken cancellationToken = default);

        void Update(Student student);

        void Delete(Student student);

        Task<bool> ExistsAsync(int studentId, CancellationToken cancellationToken = default);

        Task SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
