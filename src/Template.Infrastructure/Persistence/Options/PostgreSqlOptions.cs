namespace Template.Infrastructure.Persistence.Options;

/// <summary>
/// Параметры подключения к базе данных PostgreSQL, которые читаются из
/// конфигурации (например, <c>appsettings.json</c> или переменных окружения).
/// Эти настройки передаются в <see cref="DbContextOptionsBuilder"/> при
/// построении <see cref="DbContext"/> в инфраструктурном слое.
/// </summary>
public class PostgreSqlOptions
{
    /// <summary>
    /// Строка подключения к базе данных (включает хост, порт, имя базы,
    /// пользователь и, при необходимости, другие параметры).
    /// Пример: <c>Host=localhost;Port=5432;Database=TemplateDb;Username=app;</c>
    /// </summary>
    public string ConnectionString { get; set; } = default!;

    /// <summary>
    /// Пароль для пользователя, указанного в <see cref="ConnectionString"/>.
    /// Хранится отдельно, чтобы его можно было переопределять через
    /// защищённые хранилища (секреты, переменные окружения и т.п.).
    /// </summary>
    public string DbPassword { get; set; } = default!;
}
