using StudentManagementSystem.Application.DTOs.Faculty;

namespace StudentManagementSystem.Application.Interfaces
{
    public interface IFacultyService
    {
        Task<IEnumerable<FacultyDto>> GetAllFacultiesAsync(CancellationToken cancellationToken = default);

        Task<FacultyDto> GetFacultyByIdAsync(int facultyId, CancellationToken cancellationToken = default);

        Task<FacultyDto> CreateFacultyAsync(CreateFacultyDto dto, CancellationToken cancellationToken = default);

        Task UpdateFacultyAsync(int facultyId, UpdateFacultyDto dto, CancellationToken cancellationToken = default);

        Task DeleteFacultyAsync(int facultyId, CancellationToken cancellationToken = default);
    }
}
