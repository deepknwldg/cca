namespace Template.Application.Models.Enrollments;

/// <summary>
/// DTO, возвращаемый после успешной записи пользователя на курс.
/// </summary>
/// <param name="UserId">Идентификатор пользователя.</param>
/// <param name="CourseId">Идентификатор курса.</param>
/// <param name="EnrolledAt">Дата и время записи.</param>
public record EnrollmentResultDto(
    Guid UserId,
    Guid CourseId,
    DateTime EnrolledAt
);
