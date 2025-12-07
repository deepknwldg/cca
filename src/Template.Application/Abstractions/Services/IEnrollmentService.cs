using Template.Application.Models.Enrollments;

namespace Template.Application.Abstractions.Services;

public interface IEnrollmentService
{
    Task<EnrollmentResultDto> EnrollAsync(EnrollUserDto dto);
    Task<bool> RemoveAsync(Guid userId, Guid courseId);
}
