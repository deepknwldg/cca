using Microsoft.EntityFrameworkCore;
using Template.Application.Abstractions.Persistence.Repositories;
using Template.Domain.Entities;

namespace Template.Infrastructure.Persistence.Repositories;

/// <summary>
/// Репозиторий для работы с сущностью <see cref="Course"/>.  
/// Наследует базовый <see cref="Repository{Course}"/> и добавляет метод
/// для получения курса вместе с его уроками (eager‑loading).
/// </summary>
public class CourseRepository : Repository<Course>, ICourseRepository
{
    /// <summary>
    /// Создаёт репозиторий, получая контекст <see cref="ApplicationDbContext"/>.
    /// </summary>
    /// <param name="db">Экземпляр контекста БД.</param>
    public CourseRepository(ApplicationDbContext db)
        : base(db)
    {
    }

    /// <inheritdoc />
    public async Task<Course?> GetWithLessonsAsync(Guid id)
    {
        return await _db.Courses
            .Include(c => c.Lessons)
            .FirstOrDefaultAsync(c => c.Id == id);
    }
}
