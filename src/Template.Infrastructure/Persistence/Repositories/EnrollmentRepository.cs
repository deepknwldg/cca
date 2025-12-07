using Microsoft.EntityFrameworkCore;
using Template.Application.Abstractions.Persistence.Repositories;
using Template.Domain.Entities;

namespace Template.Infrastructure.Persistence.Repositories;

public class EnrollmentRepository : IEnrollmentRepository
{
    private readonly ApplicationDbContext _db;

    public EnrollmentRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task AddAsync(Enrollment enrollment)
    {
        await _db.Enrollments.AddAsync(enrollment);
    }

    public Task<Enrollment?> GetAsync(Guid userId, Guid courseId)
    {
        return _db.Enrollments
            .FirstOrDefaultAsync(x => x.UserId == userId && x.CourseId == courseId);
    }

    public void Remove(Enrollment enrollment)
    {
        _db.Enrollments.Remove(enrollment);
    }
}
