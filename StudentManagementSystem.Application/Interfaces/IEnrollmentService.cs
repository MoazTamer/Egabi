using StudentManagementSystem.Application.DTOs.Enrollment;

namespace StudentManagementSystem.Application.Interfaces
{
    public interface IEnrollmentService
    {
        Task<IEnumerable<EnrollmentDto>> GetAllEnrollmentsAsync(CancellationToken cancellationToken = default);

        Task<EnrollmentDto> CreateEnrollmentAsync(CreateEnrollmentDto dto, CancellationToken cancellationToken = default);

        Task DeleteEnrollmentAsync(int studentId, int courseId, CancellationToken cancellationToken = default);
    }
}
