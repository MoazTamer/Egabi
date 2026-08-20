using Microsoft.AspNetCore.Mvc;
using StudentManagementSystem.Application.DTOs.Reports;
using StudentManagementSystem.Application.Interfaces;

namespace StudentManagementSystem.API.Controllers
{
    [ApiController]
    [Route("api/reports")]
    [Produces("application/json")]
    public class ReportsController : ControllerBase
    {
        private readonly IReportService _reportService;

        public ReportsController(IReportService reportService)
        {
            _reportService = reportService;
        }

        [HttpGet("students-by-faculty")]
        [ProducesResponseType(typeof(IEnumerable<StudentsByFacultyDto>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<StudentsByFacultyDto>>> GetStudentsByFaculty(CancellationToken cancellationToken)
        {
            var report = await _reportService.GetStudentsByFacultyAsync(cancellationToken);
            return Ok(report);
        }

        [HttpGet("average-gpa-by-faculty")]
        [ProducesResponseType(typeof(IEnumerable<AverageGpaByFacultyDto>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<AverageGpaByFacultyDto>>> GetAverageGpaByFaculty(CancellationToken cancellationToken)
        {
            var report = await _reportService.GetAverageGpaByFacultyAsync(cancellationToken);
            return Ok(report);
        }

        [HttpGet("courses-with-student-count")]
        [ProducesResponseType(typeof(IEnumerable<CourseStudentCountDto>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<CourseStudentCountDto>>> GetCoursesWithStudentCount(CancellationToken cancellationToken)
        {
            var report = await _reportService.GetCoursesWithStudentCountAsync(cancellationToken);
            return Ok(report);
        }

        [HttpGet("student-courses")]
        [ProducesResponseType(typeof(IEnumerable<StudentCourseDto>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<StudentCourseDto>>> GetStudentCourses(CancellationToken cancellationToken)
        {
            var report = await _reportService.GetStudentCoursesAsync(cancellationToken);
            return Ok(report);
        }

        [HttpGet("excellent-students")]
        [ProducesResponseType(typeof(IEnumerable<ExcellentStudentDto>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<ExcellentStudentDto>>> GetExcellentStudents(CancellationToken cancellationToken)
        {
            var report = await _reportService.GetExcellentStudentsAsync(cancellationToken);
            return Ok(report);
        }
    }
}
