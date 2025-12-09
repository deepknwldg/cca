using FluentValidation;
using Template.Application.Models.Lessons;

namespace Template.Application.Validators.Lessons;

public class UpdateLessonValidator : AbstractValidator<UpdateLessonDto>
{
    public UpdateLessonValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty()
            .MaximumLength(200);

        RuleFor(x => x.Content)
            .NotEmpty();
    }
}
