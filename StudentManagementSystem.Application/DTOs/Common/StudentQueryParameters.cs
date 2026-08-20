namespace StudentManagementSystem.Application.DTOs.Common
{
    public class StudentQueryParameters
    {
        private const int MaxPageSize = 50;
        private int _pageSize = 10;

        public int PageNumber { get; set; } = 1;

        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = value > MaxPageSize ? MaxPageSize : (value < 1 ? 1 : value);
        }

        public string? Search { get; set; }
        public int? FacultyId { get; set; }
        public double? MinGpa { get; set; }
        public double? MaxGpa { get; set; }
    }
}
