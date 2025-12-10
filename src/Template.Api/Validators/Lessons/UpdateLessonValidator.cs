using FluentValidation;
using Template.Api.Models.Lesson;

namespace Template.Application.Validators.Lessons;

/// <summary>
/// Валидатор <see cref="UpdateLessonRequest"/>. Требует заполненные
/// поля <c>Title</c> и <c>Content</c> и ограничивает длину заголовка.
/// </summary>
public class UpdateLessonValidator : AbstractValidator<UpdateLessonRequest>
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
