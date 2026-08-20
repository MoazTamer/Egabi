using StudentManagementSystem.Application.DTOs.Reports;

namespace StudentManagementSystem.Application.Interfaces
{
    public interface IReportService
    {
        Task<IEnumerable<StudentsByFacultyDto>> GetStudentsByFacultyAsync(CancellationToken cancellationToken = default);

        Task<IEnumerable<AverageGpaByFacultyDto>> GetAverageGpaByFacultyAsync(CancellationToken cancellationToken = default);

        Task<IEnumerable<CourseStudentCountDto>> GetCoursesWithStudentCountAsync(CancellationToken cancellationToken = default);

        Task<IEnumerable<StudentCourseDto>> GetStudentCoursesAsync(CancellationToken cancellationToken = default);

        Task<IEnumerable<ExcellentStudentDto>> GetExcellentStudentsAsync(CancellationToken cancellationToken = default);
    }
}
