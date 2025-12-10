using FluentValidation;
using Template.Api.Models.Course;

namespace Template.Api.Validators.Courses;

/// <summary>
/// Валидатор <see cref="CreateCourseRequest"/> с бизнес‑правилами для создания курса.
/// </summary>
public class CreateCourseValidator : AbstractValidator<CreateCourseRequest>
{
    public CreateCourseValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty()
            .MaximumLength(200);

        RuleFor(x => x.Description)
            .MaximumLength(2000);
    }
}
