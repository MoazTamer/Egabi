using StudentManagementSystem.Application.Common.Exceptions;
using StudentManagementSystem.Application.DTOs.Course;
using StudentManagementSystem.Application.Interfaces;
using StudentManagementSystem.Domain.Entities;
using StudentManagementSystem.Domain.Interfaces;

namespace StudentManagementSystem.Application.Services
{
    public class CourseService : ICourseService
    {
        private readonly ICourseRepository _courseRepository;

        public CourseService(ICourseRepository courseRepository)
        {
            _courseRepository = courseRepository;
        }

        public async Task<IEnumerable<CourseDto>> GetAllCoursesAsync(CancellationToken cancellationToken = default)
        {
            var courses = await _courseRepository.GetAllAsync(cancellationToken);
            return courses.Select(MapToDto);
        }

        public async Task<CourseDto> GetCourseByIdAsync(int courseId, CancellationToken cancellationToken = default)
        {
            var course = await _courseRepository.GetByIdAsync(courseId, cancellationToken)
                ?? throw new NotFoundException(nameof(Course), courseId);

            return MapToDto(course);
        }

        public async Task<CourseDto> CreateCourseAsync(CreateCourseDto dto, CancellationToken cancellationToken = default)
        {
            var course = new Course
            {
                CourseName = dto.CourseName,
                Credits = dto.Credits
            };

            await _courseRepository.AddAsync(course, cancellationToken);
            await _courseRepository.SaveChangesAsync(cancellationToken);

            return MapToDto(course);
        }

        public async Task UpdateCourseAsync(int courseId, UpdateCourseDto dto, CancellationToken cancellationToken = default)
        {
            var course = await _courseRepository.GetByIdAsync(courseId, cancellationToken)
                ?? throw new NotFoundException(nameof(Course), courseId);

            course.CourseName = dto.CourseName;
            course.Credits = dto.Credits;

            _courseRepository.Update(course);
            await _courseRepository.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteCourseAsync(int courseId, CancellationToken cancellationToken = default)
        {
            var course = await _courseRepository.GetByIdAsync(courseId, cancellationToken)
                ?? throw new NotFoundException(nameof(Course), courseId);

            _courseRepository.Delete(course);
            await _courseRepository.SaveChangesAsync(cancellationToken);
        }

        private static CourseDto MapToDto(Course course) => new()
        {
            CourseId = course.CourseId,
            CourseName = course.CourseName,
            Credits = course.Credits
        };
    }
}
