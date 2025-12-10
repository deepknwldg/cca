using FluentValidation;
using Template.Api.Models.User;

namespace Template.Application.Validators.Users;

/// <summary>
/// Валидатор <see cref="UpdateUserRequest"/>. Позволяет изменять
/// только имя и фамилию (оба обязательны и ограничены по длине). 
/// Другие поля (e‑mail, пароль) проверяются в отдельном
/// <see cref="CreateUserValidator"/> при регистрации.
/// </summary>
public class UpdateUserValidator : AbstractValidator<UpdateUserRequest>
{
    public UpdateUserValidator()
    {
        RuleFor(x => x.FirstName)
            .NotEmpty()
            .MaximumLength(100);

        RuleFor(x => x.LastName)
            .NotEmpty()
            .MaximumLength(100);
    }
}
