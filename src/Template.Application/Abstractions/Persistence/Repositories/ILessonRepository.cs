using Template.Domain.Entities;

namespace Template.Application.Abstractions.Persistence.Repositories;

public interface ILessonRepository : IRepository<Lesson>
{
    Task<IReadOnlyList<Lesson>> GetByCourseIdAsync(Guid courseId);
}
