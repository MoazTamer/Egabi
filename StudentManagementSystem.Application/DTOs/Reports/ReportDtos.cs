namespace StudentManagementSystem.Application.DTOs.Reports
{
    public class StudentsByFacultyDto
    {
        public string FacultyName { get; set; } = string.Empty;
        public int NumberOfStudents { get; set; }
    }

    public class AverageGpaByFacultyDto
    {
        public string FacultyName { get; set; } = string.Empty;
        public double AverageGPA { get; set; }
    }

    public class CourseStudentCountDto
    {
        public string CourseName { get; set; } = string.Empty;
        public int NumberOfStudents { get; set; }
    }

    public class StudentCourseDto
    {
        public string StudentName { get; set; } = string.Empty;
        public string CourseName { get; set; } = string.Empty;
        public DateTime EnrollmentDate { get; set; }
    }

    public class ExcellentStudentDto
    {
        public int StudentId { get; set; }
        public string StudentName { get; set; } = string.Empty;
        public double GPA { get; set; }
        public string FacultyName { get; set; } = string.Empty;
    }
}
