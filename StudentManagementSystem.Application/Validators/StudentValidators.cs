using FluentValidation;
using StudentManagementSystem.Application.DTOs.Student;

namespace StudentManagementSystem.Application.Validators
{
    public class CreateStudentDtoValidator : AbstractValidator<CreateStudentDto>
    {
        public CreateStudentDtoValidator()
        {
            RuleFor(s => s.StudentName)
                .NotEmpty().WithMessage("Student name is required.")
                .MaximumLength(100).WithMessage("Student name must not exceed 100 characters.");

            RuleFor(s => s.Age)
                .GreaterThan(0).WithMessage("Age must be greater than 0.");

            RuleFor(s => s.GPA)
                .InclusiveBetween(0, 4).WithMessage("GPA must be between 0 and 4.");

            RuleFor(s => s.FacultyId)
                .GreaterThan(0).WithMessage("A valid FacultyId is required.");
        }
    }

    public class UpdateStudentDtoValidator : AbstractValidator<UpdateStudentDto>
    {
        public UpdateStudentDtoValidator()
        {
            RuleFor(s => s.StudentName)
                .NotEmpty().WithMessage("Student name is required.")
                .MaximumLength(100).WithMessage("Student name must not exceed 100 characters.");

            RuleFor(s => s.Age)
                .GreaterThan(0).WithMessage("Age must be greater than 0.");

            RuleFor(s => s.GPA)
                .InclusiveBetween(0, 4).WithMessage("GPA must be between 0 and 4.");

            RuleFor(s => s.FacultyId)
                .GreaterThan(0).WithMessage("A valid FacultyId is required.");
        }
    }
}
