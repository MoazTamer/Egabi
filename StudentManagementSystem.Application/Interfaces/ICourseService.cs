using StudentManagementSystem.Application.DTOs.Course;

namespace StudentManagementSystem.Application.Interfaces
{
    public interface ICourseService
    {
        Task<IEnumerable<CourseDto>> GetAllCoursesAsync(CancellationToken cancellationToken = default);

        Task<CourseDto> GetCourseByIdAsync(int courseId, CancellationToken cancellationToken = default);

        Task<CourseDto> CreateCourseAsync(CreateCourseDto dto, CancellationToken cancellationToken = default);

        Task UpdateCourseAsync(int courseId, UpdateCourseDto dto, CancellationToken cancellationToken = default);

        Task DeleteCourseAsync(int courseId, CancellationToken cancellationToken = default);
    }
}
