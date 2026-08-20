using Microsoft.EntityFrameworkCore;
using StudentManagementSystem.Application.Common.Exceptions;
using StudentManagementSystem.Application.DTOs.Common;
using StudentManagementSystem.Application.DTOs.Student;
using StudentManagementSystem.Application.Interfaces;
using StudentManagementSystem.Domain.Entities;
using StudentManagementSystem.Domain.Interfaces;

namespace StudentManagementSystem.Application.Services
{
    public class StudentService : IStudentService
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IFacultyRepository _facultyRepository;

        public StudentService(IStudentRepository studentRepository, IFacultyRepository facultyRepository)
        {
            _studentRepository = studentRepository;
            _facultyRepository = facultyRepository;
        }

        public async Task<PagedResult<StudentDto>> GetStudentsAsync(
            StudentQueryParameters parameters,
            CancellationToken cancellationToken = default)
        {
            var query = _studentRepository.GetQueryable();

            if (!string.IsNullOrWhiteSpace(parameters.Search))
            {
                query = query.Where(s => s.StudentName.Contains(parameters.Search));
            }

            if (parameters.FacultyId.HasValue)
            {
                query = query.Where(s => s.FacultyId == parameters.FacultyId.Value);
            }

            if (parameters.MinGpa.HasValue)
            {
                query = query.Where(s => s.GPA >= parameters.MinGpa.Value);
            }

            if (parameters.MaxGpa.HasValue)
            {
                query = query.Where(s => s.GPA <= parameters.MaxGpa.Value);
            }

            var totalCount = await query.CountAsync(cancellationToken);

            var students = await query
                .OrderBy(s => s.StudentId) 
                .Skip((parameters.PageNumber - 1) * parameters.PageSize)
                .Take(parameters.PageSize)
                .Select(s => new StudentDto
                {
                    StudentId = s.StudentId,
                    StudentName = s.StudentName,
                    Age = s.Age,
                    GPA = s.GPA,
                    FacultyId = s.FacultyId,
                    FacultyName = s.Faculty.FacultyName
                })
                .ToListAsync(cancellationToken);

            return new PagedResult<StudentDto>
            {
                Data = students,
                PageNumber = parameters.PageNumber,
                PageSize = parameters.PageSize,
                TotalCount = totalCount
            };
        }

        public async Task<StudentDto> GetStudentByIdAsync(int studentId, CancellationToken cancellationToken = default)
        {
            var student = await _studentRepository.GetByIdWithDetailsAsync(studentId, cancellationToken)
                ?? throw new NotFoundException(nameof(Student), studentId);

            return MapToDto(student);
        }

        public async Task<StudentDto> CreateStudentAsync(CreateStudentDto dto, CancellationToken cancellationToken = default)
        {
            var facultyExists = await _facultyRepository.ExistsAsync(dto.FacultyId, cancellationToken);
            if (!facultyExists)
            {
                throw new NotFoundException(nameof(Faculty), dto.FacultyId);
            }

            var student = new Student
            {
                StudentName = dto.StudentName,
                Age = dto.Age,
                GPA = dto.GPA,
                FacultyId = dto.FacultyId
            };

            await _studentRepository.AddAsync(student, cancellationToken);
            await _studentRepository.SaveChangesAsync(cancellationToken);

            var created = await _studentRepository.GetByIdWithDetailsAsync(student.StudentId, cancellationToken);
            return MapToDto(created!);
        }

        public async Task UpdateStudentAsync(int studentId, UpdateStudentDto dto, CancellationToken cancellationToken = default)
        {
            var student = await _studentRepository.GetByIdAsync(studentId, cancellationToken)
                ?? throw new NotFoundException(nameof(Student), studentId);

            var facultyExists = await _facultyRepository.ExistsAsync(dto.FacultyId, cancellationToken);
            if (!facultyExists)
            {
                throw new NotFoundException(nameof(Faculty), dto.FacultyId);
            }

            student.StudentName = dto.StudentName;
            student.Age = dto.Age;
            student.GPA = dto.GPA;
            student.FacultyId = dto.FacultyId;

            _studentRepository.Update(student);
            await _studentRepository.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteStudentAsync(int studentId, CancellationToken cancellationToken = default)
        {
            var student = await _studentRepository.GetByIdAsync(studentId, cancellationToken)
                ?? throw new NotFoundException(nameof(Student), studentId);

            _studentRepository.Delete(student);
            await _studentRepository.SaveChangesAsync(cancellationToken);
        }

        private static StudentDto MapToDto(Student student) => new()
        {
            StudentId = student.StudentId,
            StudentName = student.StudentName,
            Age = student.Age,
            GPA = student.GPA,
            FacultyId = student.FacultyId,
            FacultyName = student.Faculty != null ? student.Faculty.FacultyName : string.Empty
        };
    }
}
