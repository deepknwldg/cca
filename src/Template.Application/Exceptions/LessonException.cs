namespace Template.Application.Exceptions;

public class LessonException : ApplicationException
{
    public LessonException()
    {
    }

    public LessonException(string? message)
        : base(message) { }

    public LessonException(string? message, Exception? innerException)
        : base(message, innerException) { }
}
