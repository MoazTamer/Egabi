using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using StudentManagementSystem.Application.DTOs.Faculty;
using StudentManagementSystem.Application.Interfaces;

namespace StudentManagementSystem.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class FacultiesController : ControllerBase
    {
        private readonly IFacultyService _facultyService;
        private readonly IValidator<CreateFacultyDto> _createValidator;
        private readonly IValidator<UpdateFacultyDto> _updateValidator;

        public FacultiesController(
            IFacultyService facultyService,
            IValidator<CreateFacultyDto> createValidator,
            IValidator<UpdateFacultyDto> updateValidator)
        {
            _facultyService = facultyService;
            _createValidator = createValidator;
            _updateValidator = updateValidator;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<FacultyDto>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<FacultyDto>>> GetFaculties(CancellationToken cancellationToken)
        {
            var faculties = await _facultyService.GetAllFacultiesAsync(cancellationToken);
            return Ok(faculties);
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(typeof(FacultyDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<FacultyDto>> GetFacultyById(int id, CancellationToken cancellationToken)
        {
            var faculty = await _facultyService.GetFacultyByIdAsync(id, cancellationToken);
            return Ok(faculty);
        }

        [HttpPost]
        [ProducesResponseType(typeof(FacultyDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<FacultyDto>> CreateFaculty(
            [FromBody] CreateFacultyDto dto,
            CancellationToken cancellationToken)
        {
            await _createValidator.ValidateAndThrowAsync(dto, cancellationToken);

            var created = await _facultyService.CreateFacultyAsync(dto, cancellationToken);

            return CreatedAtAction(nameof(GetFacultyById), new { id = created.FacultyId }, created);
        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateFaculty(
            int id,
            [FromBody] UpdateFacultyDto dto,
            CancellationToken cancellationToken)
        {
            await _updateValidator.ValidateAndThrowAsync(dto, cancellationToken);

            await _facultyService.UpdateFacultyAsync(id, dto, cancellationToken);

            return NoContent();
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<IActionResult> DeleteFaculty(int id, CancellationToken cancellationToken)
        {
            await _facultyService.DeleteFacultyAsync(id, cancellationToken);
            return NoContent();
        }
    }
}
