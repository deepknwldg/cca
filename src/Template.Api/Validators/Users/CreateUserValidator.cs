using FluentValidation;
using Template.Api.Models.User;

namespace Template.Application.Validators.Users;

/// <summary>
/// Валидатор <see cref="CreateUserRequest"/>. Проверяет корректность
/// e‑mail, наличие пароля и ограничивает длину имени и фамилии.
/// </summary>
public class CreateUserValidator : AbstractValidator<CreateUserRequest>
{
    public CreateUserValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty()
            .EmailAddress();

        RuleFor(x => x.Password)
            .NotEmpty();

        RuleFor(x => x.FirstName)
            .NotEmpty()
            .MaximumLength(100);

        RuleFor(x => x.LastName)
            .NotEmpty()
            .MaximumLength(100);
    }
}
