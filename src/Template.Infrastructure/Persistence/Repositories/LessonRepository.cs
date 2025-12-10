using Microsoft.EntityFrameworkCore;
using Template.Application.Abstractions.Persistence.Repositories;
using Template.Domain.Entities;

namespace Template.Infrastructure.Persistence.Repositories;

/// <summary>
/// Репозиторий для работы с сущностью <see cref="Lesson"/>.  
/// Наследует базовый <see cref="Repository{Lesson}"/> и добавляет метод
/// получения всех уроков конкретного курса.
/// </summary>
public class LessonRepository : Repository<Lesson>, ILessonRepository
{
    /// <summary>
    /// Конструктор, получает контекст БД через DI.
    /// </summary>
    /// <param name="db">Экземпляр <see cref="ApplicationDbContext"/>.</param>
    public LessonRepository(ApplicationDbContext db) : base(db) { }

    /// <inheritdoc />
    public async Task<IReadOnlyList<Lesson>> GetByCourseIdAsync(Guid courseId)
    {
        return await _db.Lessons
            .Where(x => x.CourseId == courseId)
            .OrderBy(x => x.Title)
            .ToListAsync();
    }
}
