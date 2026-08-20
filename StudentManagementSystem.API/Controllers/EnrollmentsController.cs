using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using StudentManagementSystem.Application.DTOs.Enrollment;
using StudentManagementSystem.Application.Interfaces;

namespace StudentManagementSystem.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class EnrollmentsController : ControllerBase
    {
        private readonly IEnrollmentService _enrollmentService;
        private readonly IValidator<CreateEnrollmentDto> _createValidator;

        public EnrollmentsController(
            IEnrollmentService enrollmentService,
            IValidator<CreateEnrollmentDto> createValidator)
        {
            _enrollmentService = enrollmentService;
            _createValidator = createValidator;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<EnrollmentDto>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<EnrollmentDto>>> GetEnrollments(CancellationToken cancellationToken)
        {
            var enrollments = await _enrollmentService.GetAllEnrollmentsAsync(cancellationToken);
            return Ok(enrollments);
        }

        [HttpPost]
        [ProducesResponseType(typeof(EnrollmentDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<ActionResult<EnrollmentDto>> CreateEnrollment(
            [FromBody] CreateEnrollmentDto dto,
            CancellationToken cancellationToken)
        {
            await _createValidator.ValidateAndThrowAsync(dto, cancellationToken);

            var created = await _enrollmentService.CreateEnrollmentAsync(dto, cancellationToken);

            return StatusCode(StatusCodes.Status201Created, created);
        }

        [HttpDelete("{studentId:int}/{courseId:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteEnrollment(
            int studentId,
            int courseId,
            CancellationToken cancellationToken)
        {
            await _enrollmentService.DeleteEnrollmentAsync(studentId, courseId, cancellationToken);
            return NoContent();
        }
    }
}
