namespace Template.Api.Models.Enrollments;

public class EnrollUserRequest
{
    public Guid UserId { get; set; }
    public Guid CourseId { get; set; }
}
