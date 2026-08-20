using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using StudentManagementSystem.Application.DTOs.Common;
using StudentManagementSystem.Application.DTOs.Student;
using StudentManagementSystem.Application.Interfaces;

namespace StudentManagementSystem.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class StudentsController : ControllerBase
    {
        private readonly IStudentService _studentService;
        private readonly IValidator<CreateStudentDto> _createValidator;
        private readonly IValidator<UpdateStudentDto> _updateValidator;

        public StudentsController(
            IStudentService studentService,
            IValidator<CreateStudentDto> createValidator,
            IValidator<UpdateStudentDto> updateValidator)
        {
            _studentService = studentService;
            _createValidator = createValidator;
            _updateValidator = updateValidator;
        }


        [HttpGet]
        [ProducesResponseType(typeof(PagedResult<StudentDto>), StatusCodes.Status200OK)]
        public async Task<ActionResult<PagedResult<StudentDto>>> GetStudents(
            [FromQuery] StudentQueryParameters parameters,
            CancellationToken cancellationToken)
        {
            var result = await _studentService.GetStudentsAsync(parameters, cancellationToken);
            return Ok(result);
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(typeof(StudentDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<StudentDto>> GetStudentById(int id, CancellationToken cancellationToken)
        {
            var student = await _studentService.GetStudentByIdAsync(id, cancellationToken);
            return Ok(student);
        }

        [HttpPost]
        [ProducesResponseType(typeof(StudentDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<StudentDto>> CreateStudent(
            [FromBody] CreateStudentDto dto,
            CancellationToken cancellationToken)
        {
            await _createValidator.ValidateAndThrowAsync(dto, cancellationToken);

            var created = await _studentService.CreateStudentAsync(dto, cancellationToken);

            return CreatedAtAction(nameof(GetStudentById), new { id = created.StudentId }, created);
        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateStudent(
            int id,
            [FromBody] UpdateStudentDto dto,
            CancellationToken cancellationToken)
        {
            await _updateValidator.ValidateAndThrowAsync(dto, cancellationToken);

            await _studentService.UpdateStudentAsync(id, dto, cancellationToken);

            return NoContent();
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteStudent(int id, CancellationToken cancellationToken)
        {
            await _studentService.DeleteStudentAsync(id, cancellationToken);
            return NoContent();
        }
    }
}
