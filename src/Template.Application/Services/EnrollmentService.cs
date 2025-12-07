using Mapster;
using Template.Application.Abstractions.Persistence.Repositories;
using Template.Application.Abstractions.Services;
using Template.Application.Models.Enrollments;
using Template.Domain.Entities;

namespace Template.Application.Services;

public class EnrollmentService : IEnrollmentService
{
    private readonly IEnrollmentRepository _enrollmentRepo;
    private readonly IUserRepository _userRepo;
    private readonly ICourseRepository _courseRepo;
    private readonly IUnitOfWork _uow;

    public EnrollmentService(
        IEnrollmentRepository enrollmentRepo,
        IUserRepository userRepo,
        ICourseRepository courseRepo,
        IUnitOfWork uow)
    {
        _enrollmentRepo = enrollmentRepo;
        _userRepo = userRepo;
        _courseRepo = courseRepo;
        _uow = uow;
    }

    public async Task<EnrollmentResultDto> EnrollAsync(EnrollUserDto dto)
    {
        await _uow.BeginTransactionAsync();

        try
        {
            var user = await _userRepo.GetByIdAsync(dto.UserId);
            if (user == null)
                throw new InvalidOperationException("User not found");

            var course = await _courseRepo.GetByIdAsync(dto.CourseId);
            if (course == null)
                throw new InvalidOperationException("Course not found");

            var enrollment = new Enrollment
            {
                UserId = dto.UserId,
                CourseId = dto.CourseId,
                EnrolledAt = DateTime.UtcNow
            };

            await _enrollmentRepo.AddAsync(enrollment);
            await _uow.SaveChangesAsync();

            await _uow.CommitAsync();
            return enrollment.Adapt<EnrollmentResultDto>();
        }
        catch
        {
            await _uow.RollbackAsync();
            throw;
        }
    }

    public async Task<bool> RemoveAsync(Guid userId, Guid courseId)
    {
        await _uow.BeginTransactionAsync();

        try
        {
            var entity = await _enrollmentRepo.GetAsync(userId, courseId);
            if (entity == null) return false;

            _enrollmentRepo.Remove(entity);
            await _uow.SaveChangesAsync();

            await _uow.CommitAsync();
            return true;
        }
        catch
        {
            await _uow.RollbackAsync();
            throw;
        }
    }
}
