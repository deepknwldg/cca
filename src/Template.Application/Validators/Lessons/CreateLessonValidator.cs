using FluentValidation;
using Template.Application.Models.Lessons;

namespace Template.Application.Validators.Lessons;

public class CreateLessonValidator : AbstractValidator<CreateLessonDto>
{
    public CreateLessonValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty()
            .MaximumLength(200);

        RuleFor(x => x.Content)
            .NotEmpty();

        RuleFor(x => x.CourseId)
            .NotEmpty();
    }
}
