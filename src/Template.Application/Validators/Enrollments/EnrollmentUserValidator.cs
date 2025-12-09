using FluentValidation;
using Template.Application.Models.Enrollments;

namespace Template.Application.Validators.Enrollments;

public class EnrollUserValidator : AbstractValidator<EnrollUserDto>
{
    public EnrollUserValidator()
    {
        RuleFor(x => x.UserId)
            .NotEmpty();

        RuleFor(x => x.CourseId)
            .NotEmpty();
    }
}
