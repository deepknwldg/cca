using FluentValidation;
using Template.Api.Models.Enrollments;

namespace Template.Application.Validators.Enrollments;

/// <summary>
/// Валидатор <see cref="EnrollUserRequest"/>. Требует, чтобы оба
/// идентификатора (пользователь и курс) были заданы (не равны <c>Guid.Empty</c>).
/// </summary>
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
