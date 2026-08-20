namespace StudentManagementSystem.Application.DTOs.Course
{
    public class CourseDto
    {
        public int CourseId { get; set; }
        public string CourseName { get; set; } = string.Empty;
        public int Credits { get; set; }
    }

    public class CreateCourseDto
    {
        public string CourseName { get; set; } = string.Empty;
        public int Credits { get; set; }
    }

    public class UpdateCourseDto
    {
        public string CourseName { get; set; } = string.Empty;
        public int Credits { get; set; }
    }
}
