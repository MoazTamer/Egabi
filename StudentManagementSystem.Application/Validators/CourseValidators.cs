using FluentValidation;
using StudentManagementSystem.Application.DTOs.Course;

namespace StudentManagementSystem.Application.Validators
{
    public class CreateCourseDtoValidator : AbstractValidator<CreateCourseDto>
    {
        public CreateCourseDtoValidator()
        {
            RuleFor(c => c.CourseName)
                .NotEmpty().WithMessage("Course name is required.")
                .MaximumLength(150).WithMessage("Course name must not exceed 150 characters.");

            RuleFor(c => c.Credits)
                .GreaterThan(0).WithMessage("Credits must be greater than 0.");
        }
    }

    public class UpdateCourseDtoValidator : AbstractValidator<UpdateCourseDto>
    {
        public UpdateCourseDtoValidator()
        {
            RuleFor(c => c.CourseName)
                .NotEmpty().WithMessage("Course name is required.")
                .MaximumLength(150).WithMessage("Course name must not exceed 150 characters.");

            RuleFor(c => c.Credits)
                .GreaterThan(0).WithMessage("Credits must be greater than 0.");
        }
    }
}
