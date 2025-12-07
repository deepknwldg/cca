using Template.Domain.Entities;

namespace Template.Application.Abstractions.Persistence.Repositories;

public interface ICourseRepository : IRepository<Course>
{
    Task<Course?> GetWithLessonsAsync(Guid id);
}
