using FluentValidation;
using StudentManagementSystem.Application.DTOs.Faculty;

namespace StudentManagementSystem.Application.Validators
{
    public class CreateFacultyDtoValidator : AbstractValidator<CreateFacultyDto>
    {
        public CreateFacultyDtoValidator()
        {
            RuleFor(f => f.FacultyName)
                .NotEmpty().WithMessage("Faculty name is required.")
                .MaximumLength(100).WithMessage("Faculty name must not exceed 100 characters.");
        }
    }

    public class UpdateFacultyDtoValidator : AbstractValidator<UpdateFacultyDto>
    {
        public UpdateFacultyDtoValidator()
        {
            RuleFor(f => f.FacultyName)
                .NotEmpty().WithMessage("Faculty name is required.")
                .MaximumLength(100).WithMessage("Faculty name must not exceed 100 characters.");
        }
    }
}
