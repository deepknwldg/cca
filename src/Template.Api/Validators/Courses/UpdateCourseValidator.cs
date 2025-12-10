using FluentValidation;
using Template.Api.Models.Course;

namespace Template.Application.Validators.Courses;

/// <summary>
/// Валидатор <see cref="UpdateCourseRequest"/>. Проверяет,
/// что обязательные поля заполнены и не превышают допустимую длину.
/// </summary>
public class UpdateCourseValidator : AbstractValidator<UpdateCourseRequest>
{
    public UpdateCourseValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty()
            .MaximumLength(200);

        RuleFor(x => x.Description)
            .MaximumLength(2000);
    }
}
