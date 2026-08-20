using Microsoft.EntityFrameworkCore;
using StudentManagementSystem.Application.DTOs.Reports;
using StudentManagementSystem.Application.Interfaces;
using StudentManagementSystem.Infrastructure.Data;

namespace StudentManagementSystem.Infrastructure.Repositories
{
    public class ReportRepository : IReportRepository
    {
        private readonly StudentManagementDbContext _context;

        public ReportRepository(StudentManagementDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<StudentsByFacultyDto>> GetStudentsByFacultyAsync(CancellationToken cancellationToken = default)
        {
            return await _context.Faculties
                .AsNoTracking()
                .Select(f => new StudentsByFacultyDto
                {
                    FacultyName = f.FacultyName,
                    NumberOfStudents = f.Students.Count
                })
                .ToListAsync(cancellationToken);
        }

        public async Task<IEnumerable<AverageGpaByFacultyDto>> GetAverageGpaByFacultyAsync(CancellationToken cancellationToken = default)
        {
            return await _context.Faculties
                .AsNoTracking()
                .Select(f => new AverageGpaByFacultyDto
                {
                    FacultyName = f.FacultyName,
                    AverageGPA = f.Students.Any() ? f.Students.Average(s => s.GPA) : 0
                })
                .ToListAsync(cancellationToken);
        }

        public async Task<IEnumerable<CourseStudentCountDto>> GetCoursesWithStudentCountAsync(CancellationToken cancellationToken = default)
        {
            return await _context.Courses
                .AsNoTracking()
                .Select(c => new CourseStudentCountDto
                {
                    CourseName = c.CourseName,
                    NumberOfStudents = c.Enrollments.Count
                })
                .ToListAsync(cancellationToken);
        }

        public async Task<IEnumerable<StudentCourseDto>> GetStudentCoursesAsync(CancellationToken cancellationToken = default)
        {
            return await _context.Enrollments
                .AsNoTracking()
                .OrderBy(e => e.Student.StudentName)
                .Select(e => new StudentCourseDto
                {
                    StudentName = e.Student.StudentName,
                    CourseName = e.Course.CourseName,
                    EnrollmentDate = e.EnrollmentDate
                })
                .ToListAsync(cancellationToken);
        }

        public async Task<IEnumerable<ExcellentStudentDto>> GetExcellentStudentsAsync(double minGpa, CancellationToken cancellationToken = default)
        {
            return await _context.Students
                .AsNoTracking()
                .Where(s => s.GPA >= minGpa)
                .OrderByDescending(s => s.GPA)
                .Select(s => new ExcellentStudentDto
                {
                    StudentId = s.StudentId,
                    StudentName = s.StudentName,
                    GPA = s.GPA,
                    FacultyName = s.Faculty.FacultyName
                })
                .ToListAsync(cancellationToken);
        }
    }
}
