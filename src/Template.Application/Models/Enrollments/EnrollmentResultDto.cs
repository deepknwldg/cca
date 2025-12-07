namespace Template.Application.Models.Enrollments;

public record EnrollmentResultDto(
    Guid UserId,
    Guid CourseId,
    DateTime EnrolledAt
);
