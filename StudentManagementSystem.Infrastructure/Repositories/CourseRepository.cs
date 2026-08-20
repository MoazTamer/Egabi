using Microsoft.EntityFrameworkCore;
using StudentManagementSystem.Domain.Entities;
using StudentManagementSystem.Domain.Interfaces;
using StudentManagementSystem.Infrastructure.Data;

namespace StudentManagementSystem.Infrastructure.Repositories
{
    public class CourseRepository : ICourseRepository
    {
        private readonly StudentManagementDbContext _context;

        public CourseRepository(StudentManagementDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Course>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _context.Courses
                .AsNoTracking()
                .ToListAsync(cancellationToken);
        }

        public async Task<Course?> GetByIdAsync(int courseId, CancellationToken cancellationToken = default)
        {
            return await _context.Courses
                .FirstOrDefaultAsync(c => c.CourseId == courseId, cancellationToken);
        }

        public async Task AddAsync(Course course, CancellationToken cancellationToken = default)
        {
            await _context.Courses.AddAsync(course, cancellationToken);
        }

        public void Update(Course course)
        {
            _context.Courses.Update(course);
        }

        public void Delete(Course course)
        {
            _context.Courses.Remove(course);
        }

        public async Task<bool> ExistsAsync(int courseId, CancellationToken cancellationToken = default)
        {
            return await _context.Courses.AnyAsync(c => c.CourseId == courseId, cancellationToken);
        }

        public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
