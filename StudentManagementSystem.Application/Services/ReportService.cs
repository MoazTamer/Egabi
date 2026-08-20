using StudentManagementSystem.Application.DTOs.Reports;
using StudentManagementSystem.Application.Interfaces;

namespace StudentManagementSystem.Application.Services
{
    public class ReportService : IReportService
    {
        private readonly IReportRepository _reportRepository;

        private const double ExcellentGpaThreshold = 3.0;

        public ReportService(IReportRepository reportRepository)
        {
            _reportRepository = reportRepository;
        }

        public Task<IEnumerable<StudentsByFacultyDto>> GetStudentsByFacultyAsync(CancellationToken cancellationToken = default)
            => _reportRepository.GetStudentsByFacultyAsync(cancellationToken);

        public Task<IEnumerable<AverageGpaByFacultyDto>> GetAverageGpaByFacultyAsync(CancellationToken cancellationToken = default)
            => _reportRepository.GetAverageGpaByFacultyAsync(cancellationToken);

        public Task<IEnumerable<CourseStudentCountDto>> GetCoursesWithStudentCountAsync(CancellationToken cancellationToken = default)
            => _reportRepository.GetCoursesWithStudentCountAsync(cancellationToken);

        public Task<IEnumerable<StudentCourseDto>> GetStudentCoursesAsync(CancellationToken cancellationToken = default)
            => _reportRepository.GetStudentCoursesAsync(cancellationToken);

        public Task<IEnumerable<ExcellentStudentDto>> GetExcellentStudentsAsync(CancellationToken cancellationToken = default)
            => _reportRepository.GetExcellentStudentsAsync(ExcellentGpaThreshold, cancellationToken);
    }
}
