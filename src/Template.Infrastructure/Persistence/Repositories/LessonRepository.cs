using Microsoft.EntityFrameworkCore;
using Template.Application.Abstractions.Persistence.Repositories;
using Template.Domain.Entities;

namespace Template.Infrastructure.Persistence.Repositories;

public class LessonRepository : Repository<Lesson>, ILessonRepository
{
    public LessonRepository(ApplicationDbContext db) : base(db) { }

    public async Task<IReadOnlyList<Lesson>> GetByCourseIdAsync(Guid courseId)
    {
        return await _db.Lessons
            .Where(x => x.CourseId == courseId)
            .OrderBy(x => x.Title)
            .ToListAsync();
    }
}
