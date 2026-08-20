using Microsoft.EntityFrameworkCore;
using StudentManagementSystem.Domain.Entities;
using StudentManagementSystem.Domain.Interfaces;
using StudentManagementSystem.Infrastructure.Data;

namespace StudentManagementSystem.Infrastructure.Repositories
{
    public class EnrollmentRepository : IEnrollmentRepository
    {
        private readonly StudentManagementDbContext _context;

        public EnrollmentRepository(StudentManagementDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Enrollment>> GetAllWithDetailsAsync(CancellationToken cancellationToken = default)
        {
            return await _context.Enrollments
                .Include(e => e.Student)
                .Include(e => e.Course)
                .AsNoTracking()
                .ToListAsync(cancellationToken);
        }

        public async Task<Enrollment?> GetByIdAsync(int studentId, int courseId, CancellationToken cancellationToken = default)
        {
            return await _context.Enrollments
                .FirstOrDefaultAsync(e => e.StudentId == studentId && e.CourseId == courseId, cancellationToken);
        }

        public async Task<bool> ExistsAsync(int studentId, int courseId, CancellationToken cancellationToken = default)
        {
            return await _context.Enrollments
                .AnyAsync(e => e.StudentId == studentId && e.CourseId == courseId, cancellationToken);
        }

        public async Task AddAsync(Enrollment enrollment, CancellationToken cancellationToken = default)
        {
            await _context.Enrollments.AddAsync(enrollment, cancellationToken);
        }

        public void Delete(Enrollment enrollment)
        {
            _context.Enrollments.Remove(enrollment);
        }

        public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
