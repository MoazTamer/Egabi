namespace StudentManagementSystem.Domain.Entities
{
    public class Faculty
    {
        public int FacultyId { get; set; }
        public string FacultyName { get; set; } = string.Empty;

        public ICollection<Student> Students { get; set; } = new List<Student>();
    }
}
