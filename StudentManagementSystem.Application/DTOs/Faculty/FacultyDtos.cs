namespace StudentManagementSystem.Application.DTOs.Faculty
{
    public class FacultyDto
    {
        public int FacultyId { get; set; }
        public string FacultyName { get; set; } = string.Empty;

        public int StudentCount { get; set; }
    }

    public class CreateFacultyDto
    {
        public string FacultyName { get; set; } = string.Empty;
    }

    public class UpdateFacultyDto
    {
        public string FacultyName { get; set; } = string.Empty;
    }
}
