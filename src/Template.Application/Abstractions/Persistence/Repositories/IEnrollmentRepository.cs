using Template.Domain.Entities;

namespace Template.Application.Abstractions.Persistence.Repositories;

//M:N сущности редко делают generic repository, нет необходимости в базовом IRepository
public interface IEnrollmentRepository
{
    Task AddAsync(Enrollment enrollment);
    Task<Enrollment?> GetAsync(Guid userId, Guid courseId);
    void Remove(Enrollment enrollment);
}
