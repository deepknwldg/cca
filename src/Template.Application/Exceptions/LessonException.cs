namespace Template.Application.Exceptions;

/// <summary>
/// Исключение, специфичное для домена уроков (Lesson).
/// Наследуется от <see cref="ApplicationException"/> и может использоваться
/// в сервисах и репозиториях для передачи бизнес‑ошибок клиенту.
/// </summary>
public class LessonException : ApplicationException
{
    /// <summary>Создаёт экземпляр без сообщения.</summary>
    public LessonException() { }

    /// <summary>
    /// Создаёт экземпляр с пользовательским сообщением.
    /// </summary>
    /// <param name="message">Текст сообщения ошибки.</param>
    public LessonException(string? message) : base(message) { }

    /// <summary>
    /// Создаёт экземпляр с сообщением и внутренним исключением.
    /// </summary>
    /// <param name="message">Текст сообщения ошибки.</param>
    /// <param name="innerException">Внутреннее исключение.</param>
    public LessonException(string? message, Exception? innerException)
        : base(message, innerException) { }
}
