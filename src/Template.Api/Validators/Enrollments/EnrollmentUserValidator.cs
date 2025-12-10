using FluentValidation;
using Template.Api.Models.Enrollments;

namespace Template.Application.Validators.Enrollments;

public class EnrollUserValidator : AbstractValidator<EnrollUserRequest>
{
    public EnrollUserValidator()
    {
        RuleFor(x => x.UserId)
            .NotEmpty();

        RuleFor(x => x.CourseId)
            .NotEmpty();
    }
}
