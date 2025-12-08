using Microsoft.EntityFrameworkCore;
using Template.Application.Abstractions.Persistence.Repositories;
using Template.Domain.Entities;

namespace Template.Infrastructure.Persistence.Repositories;

public class CourseRepository : Repository<Course>, ICourseRepository
{
    public CourseRepository(ApplicationDbContext db)
        : base(db)
    {
    }

    public async Task<Course?> GetWithLessonsAsync(Guid id)
    {
        throw new NotImplementedException();
        //return await _db.Courses
        //    .Include(c => c.Lessons)
        //    .FirstOrDefaultAsync(c => c.Id == id);
    }
}
