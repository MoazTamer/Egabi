using Microsoft.EntityFrameworkCore;
using StudentManagementSystem.Domain.Entities;
using StudentManagementSystem.Domain.Interfaces;
using StudentManagementSystem.Infrastructure.Data;

namespace StudentManagementSystem.Infrastructure.Repositories
{
    public class StudentRepository : IStudentRepository
    {
        private readonly StudentManagementDbContext _context;

        public StudentRepository(StudentManagementDbContext context)
        {
            _context = context;
        }

        public IQueryable<Student> GetQueryable()
        {
            return _context.Students
                .AsNoTracking()
                .Include(s => s.Faculty);
        }

        public async Task<Student?> GetByIdAsync(int studentId, CancellationToken cancellationToken = default)
        {
            return await _context.Students
                .FirstOrDefaultAsync(s => s.StudentId == studentId, cancellationToken);
        }

        public async Task<Student?> GetByIdWithDetailsAsync(int studentId, CancellationToken cancellationToken = default)
        {
            return await _context.Students
                .Include(s => s.Faculty)
                .AsNoTracking()
                .FirstOrDefaultAsync(s => s.StudentId == studentId, cancellationToken);
        }

        public async Task AddAsync(Student student, CancellationToken cancellationToken = default)
        {
            await _context.Students.AddAsync(student, cancellationToken);
        }

        public void Update(Student student)
        {
            _context.Students.Update(student);
        }

        public void Delete(Student student)
        {
            _context.Students.Remove(student);
        }

        public async Task<bool> ExistsAsync(int studentId, CancellationToken cancellationToken = default)
        {
            return await _context.Students.AnyAsync(s => s.StudentId == studentId, cancellationToken);
        }

        public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
