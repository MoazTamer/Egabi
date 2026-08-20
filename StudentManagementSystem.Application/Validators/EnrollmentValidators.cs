using FluentValidation;
using StudentManagementSystem.Application.DTOs.Enrollment;

namespace StudentManagementSystem.Application.Validators
{
    public class CreateEnrollmentDtoValidator : AbstractValidator<CreateEnrollmentDto>
    {
        public CreateEnrollmentDtoValidator()
        {
            RuleFor(e => e.StudentId)
                .GreaterThan(0).WithMessage("A valid StudentId is required.");

            RuleFor(e => e.CourseId)
                .GreaterThan(0).WithMessage("A valid CourseId is required.");

        }
    }
}
