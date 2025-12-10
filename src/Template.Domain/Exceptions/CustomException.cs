namespace Template.Domain.Exceptions;

/// <summary>
/// Базовое пользовательское исключение для доменного уровня. 
/// Позволяет задать собственный тип ошибки и, при необходимости, вложить внутреннее исключение.
/// </summary>
public class CustomException : Exception
{
    /// <summary>
    /// Создаёт исключение без сообщения.
    /// </summary>
    public CustomException() { }

    /// <summary>
    /// Создаёт исключение с пользовательским сообщением.
    /// </summary>
    /// <param name="message">Текст сообщения.</param>
    public CustomException(string? message) : base(message) { }

    /// <summary>
    /// Создаёт исключение с сообщением и вложенным (inner) исключением.
    /// </summary>
    /// <param name="message">Текст сообщения.</param>
    /// <param name="innerException">Внутреннее исключение.</param>
    public CustomException(string? message, Exception? innerException) : base(message, innerException) { }
}
