using Microsoft.EntityFrameworkCore;
using StudentManagementSystem.Domain.Entities;
using StudentManagementSystem.Domain.Interfaces;
using StudentManagementSystem.Infrastructure.Data;

namespace StudentManagementSystem.Infrastructure.Repositories
{
    public class FacultyRepository : IFacultyRepository
    {
        private readonly StudentManagementDbContext _context;

        public FacultyRepository(StudentManagementDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Faculty>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _context.Faculties
                .Include(f => f.Students)
                .AsNoTracking()
                .ToListAsync(cancellationToken);
        }

        public async Task<Faculty?> GetByIdAsync(int facultyId, CancellationToken cancellationToken = default)
        {
            return await _context.Faculties
                .Include(f => f.Students)
                .FirstOrDefaultAsync(f => f.FacultyId == facultyId, cancellationToken);
        }

        public async Task AddAsync(Faculty faculty, CancellationToken cancellationToken = default)
        {
            await _context.Faculties.AddAsync(faculty, cancellationToken);
        }

        public void Update(Faculty faculty)
        {
            _context.Faculties.Update(faculty);
        }

        public void Delete(Faculty faculty)
        {
            _context.Faculties.Remove(faculty);
        }

        public async Task<bool> ExistsAsync(int facultyId, CancellationToken cancellationToken = default)
        {
            return await _context.Faculties.AnyAsync(f => f.FacultyId == facultyId, cancellationToken);
        }

        public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
