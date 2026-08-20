namespace StudentManagementSystem.Domain.Entities
{
    /// <summary>
    /// Represents a student enrolled in the system.
    /// Belongs to exactly one Faculty, and can enroll in many Courses via Enrollment.
    /// </summary>
    public class Student
    {
        public int StudentId { get; set; }
        public string StudentName { get; set; } = string.Empty;
        public int Age { get; set; }
        public double GPA { get; set; }

        public int FacultyId { get; set; }
        public Faculty Faculty { get; set; } = null!;

        public ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();
    }
}
