using StudentManagementSystem.Application.DTOs.Common;
using StudentManagementSystem.Application.DTOs.Student;

namespace StudentManagementSystem.Application.Interfaces
{
    public interface IStudentService
    {
        Task<PagedResult<StudentDto>> GetStudentsAsync(StudentQueryParameters parameters, CancellationToken cancellationToken = default);

        Task<StudentDto> GetStudentByIdAsync(int studentId, CancellationToken cancellationToken = default);

        Task<StudentDto> CreateStudentAsync(CreateStudentDto dto, CancellationToken cancellationToken = default);

        Task UpdateStudentAsync(int studentId, UpdateStudentDto dto, CancellationToken cancellationToken = default);

        Task DeleteStudentAsync(int studentId, CancellationToken cancellationToken = default);
    }
}
