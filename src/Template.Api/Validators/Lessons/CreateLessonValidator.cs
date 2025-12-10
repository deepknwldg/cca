using FluentValidation;
using Template.Api.Models.Lesson;

namespace Template.Application.Validators.Lessons;

/// <summary>
/// Валидатор <see cref="CreateLessonRequest"/>. Проверяет наличие
/// обязательных полей и ограничивает длину заголовка.
/// </summary>
public class CreateLessonValidator : AbstractValidator<CreateLessonRequest>
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
