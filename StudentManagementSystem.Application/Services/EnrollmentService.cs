using StudentManagementSystem.Application.Common.Exceptions;
using StudentManagementSystem.Application.DTOs.Enrollment;
using StudentManagementSystem.Application.Interfaces;
using StudentManagementSystem.Domain.Entities;
using StudentManagementSystem.Domain.Interfaces;

namespace StudentManagementSystem.Application.Services
{
    public class EnrollmentService : IEnrollmentService
    {
        private readonly IEnrollmentRepository _enrollmentRepository;
        private readonly IStudentRepository _studentRepository;
        private readonly ICourseRepository _courseRepository;

        public EnrollmentService(
            IEnrollmentRepository enrollmentRepository,
            IStudentRepository studentRepository,
            ICourseRepository courseRepository)
        {
            _enrollmentRepository = enrollmentRepository;
            _studentRepository = studentRepository;
            _courseRepository = courseRepository;
        }

        public async Task<IEnumerable<EnrollmentDto>> GetAllEnrollmentsAsync(CancellationToken cancellationToken = default)
        {
            var enrollments = await _enrollmentRepository.GetAllWithDetailsAsync(cancellationToken);
            return enrollments.Select(MapToDto);
        }

        public async Task<EnrollmentDto> CreateEnrollmentAsync(CreateEnrollmentDto dto, CancellationToken cancellationToken = default)
        {
            var student = await _studentRepository.GetByIdAsync(dto.StudentId, cancellationToken)
                ?? throw new NotFoundException(nameof(Student), dto.StudentId);

            var course = await _courseRepository.GetByIdAsync(dto.CourseId, cancellationToken)
                ?? throw new NotFoundException(nameof(Course), dto.CourseId);

            var alreadyEnrolled = await _enrollmentRepository.ExistsAsync(dto.StudentId, dto.CourseId, cancellationToken);
            if (alreadyEnrolled)
            {
                throw new ConflictException(
                    $"Student '{dto.StudentId}' is already enrolled in course '{dto.CourseId}'.");
            }

            var enrollment = new Enrollment
            {
                StudentId = dto.StudentId,
                CourseId = dto.CourseId,
                EnrollmentDate = DateTime.UtcNow
            };

            await _enrollmentRepository.AddAsync(enrollment, cancellationToken);
            await _enrollmentRepository.SaveChangesAsync(cancellationToken);

            return new EnrollmentDto
            {
                StudentId = student.StudentId,
                StudentName = student.StudentName,
                CourseId = course.CourseId,
                CourseName = course.CourseName,
                EnrollmentDate = enrollment.EnrollmentDate
            };
        }

        public async Task DeleteEnrollmentAsync(int studentId, int courseId, CancellationToken cancellationToken = default)
        {
            var enrollment = await _enrollmentRepository.GetByIdAsync(studentId, courseId, cancellationToken)
                ?? throw new NotFoundException(nameof(Enrollment), $"{studentId}-{courseId}");

            _enrollmentRepository.Delete(enrollment);
            await _enrollmentRepository.SaveChangesAsync(cancellationToken);
        }

        private static EnrollmentDto MapToDto(Enrollment enrollment) => new()
        {
            StudentId = enrollment.StudentId,
            StudentName = enrollment.Student.StudentName,
            CourseId = enrollment.CourseId,
            CourseName = enrollment.Course.CourseName,
            EnrollmentDate = enrollment.EnrollmentDate
        };
    }
}
