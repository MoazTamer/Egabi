using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using StudentManagementSystem.Application.DTOs.Course;
using StudentManagementSystem.Application.Interfaces;

namespace StudentManagementSystem.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class CoursesController : ControllerBase
    {
        private readonly ICourseService _courseService;
        private readonly IValidator<CreateCourseDto> _createValidator;
        private readonly IValidator<UpdateCourseDto> _updateValidator;

        public CoursesController(
            ICourseService courseService,
            IValidator<CreateCourseDto> createValidator,
            IValidator<UpdateCourseDto> updateValidator)
        {
            _courseService = courseService;
            _createValidator = createValidator;
            _updateValidator = updateValidator;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<CourseDto>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<CourseDto>>> GetCourses(CancellationToken cancellationToken)
        {
            var courses = await _courseService.GetAllCoursesAsync(cancellationToken);
            return Ok(courses);
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(typeof(CourseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<CourseDto>> GetCourseById(int id, CancellationToken cancellationToken)
        {
            var course = await _courseService.GetCourseByIdAsync(id, cancellationToken);
            return Ok(course);
        }

        [HttpPost]
        [ProducesResponseType(typeof(CourseDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<CourseDto>> CreateCourse(
            [FromBody] CreateCourseDto dto,
            CancellationToken cancellationToken)
        {
            await _createValidator.ValidateAndThrowAsync(dto, cancellationToken);

            var created = await _courseService.CreateCourseAsync(dto, cancellationToken);

            return CreatedAtAction(nameof(GetCourseById), new { id = created.CourseId }, created);
        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateCourse(
            int id,
            [FromBody] UpdateCourseDto dto,
            CancellationToken cancellationToken)
        {
            await _updateValidator.ValidateAndThrowAsync(dto, cancellationToken);

            await _courseService.UpdateCourseAsync(id, dto, cancellationToken);

            return NoContent();
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<IActionResult> DeleteCourse(int id, CancellationToken cancellationToken)
        {
            await _courseService.DeleteCourseAsync(id, cancellationToken);
            return NoContent();
        }
    }
}
