using StudentManagementSystem.Application.Common.Exceptions;
using StudentManagementSystem.Application.DTOs.Faculty;
using StudentManagementSystem.Application.Interfaces;
using StudentManagementSystem.Domain.Entities;
using StudentManagementSystem.Domain.Interfaces;

namespace StudentManagementSystem.Application.Services
{
    public class FacultyService : IFacultyService
    {
        private readonly IFacultyRepository _facultyRepository;

        public FacultyService(IFacultyRepository facultyRepository)
        {
            _facultyRepository = facultyRepository;
        }

        public async Task<IEnumerable<FacultyDto>> GetAllFacultiesAsync(CancellationToken cancellationToken = default)
        {
            var faculties = await _facultyRepository.GetAllAsync(cancellationToken);
            return faculties.Select(MapToDto);
        }

        public async Task<FacultyDto> GetFacultyByIdAsync(int facultyId, CancellationToken cancellationToken = default)
        {
            var faculty = await _facultyRepository.GetByIdAsync(facultyId, cancellationToken)
                ?? throw new NotFoundException(nameof(Faculty), facultyId);

            return MapToDto(faculty);
        }

        public async Task<FacultyDto> CreateFacultyAsync(CreateFacultyDto dto, CancellationToken cancellationToken = default)
        {
            var faculty = new Faculty
            {
                FacultyName = dto.FacultyName
            };

            await _facultyRepository.AddAsync(faculty, cancellationToken);
            await _facultyRepository.SaveChangesAsync(cancellationToken);

            return MapToDto(faculty);
        }

        public async Task UpdateFacultyAsync(int facultyId, UpdateFacultyDto dto, CancellationToken cancellationToken = default)
        {
            var faculty = await _facultyRepository.GetByIdAsync(facultyId, cancellationToken)
                ?? throw new NotFoundException(nameof(Faculty), facultyId);

            faculty.FacultyName = dto.FacultyName;

            _facultyRepository.Update(faculty);
            await _facultyRepository.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteFacultyAsync(int facultyId, CancellationToken cancellationToken = default)
        {
            var faculty = await _facultyRepository.GetByIdAsync(facultyId, cancellationToken)
                ?? throw new NotFoundException(nameof(Faculty), facultyId);


            _facultyRepository.Delete(faculty);
            await _facultyRepository.SaveChangesAsync(cancellationToken);
        }

        private static FacultyDto MapToDto(Faculty faculty) => new()
        {
            FacultyId = faculty.FacultyId,
            FacultyName = faculty.FacultyName,
            StudentCount = faculty.Students?.Count ?? 0
        };
    }
}
