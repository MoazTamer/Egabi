namespace StudentManagementSystem.Application.DTOs.Student
{
    public class StudentDto
    {
        public int StudentId { get; set; }
        public string StudentName { get; set; } = string.Empty;
        public int Age { get; set; }
        public double GPA { get; set; }
        public int FacultyId { get; set; }
        public string FacultyName { get; set; } = string.Empty;
    }

    public class CreateStudentDto
    {
        public string StudentName { get; set; } = string.Empty;
        public int Age { get; set; }
        public double GPA { get; set; }
        public int FacultyId { get; set; }
    }

    public class UpdateStudentDto
    {
        public string StudentName { get; set; } = string.Empty;
        public int Age { get; set; }
        public double GPA { get; set; }
        public int FacultyId { get; set; }
    }
}
