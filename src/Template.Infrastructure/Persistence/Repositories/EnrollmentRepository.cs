using Microsoft.EntityFrameworkCore;
using Template.Application.Abstractions.Persistence.Repositories;
using Template.Domain.Entities;

namespace Template.Infrastructure.Persistence.Repositories;

/// <summary>
/// Репозиторий для работы с сущностью <see cref="Enrollment"/> (многие‑ко‑многим связь
/// между пользователем и курсом). Не наследуется от базового репозитория,
/// т.к. нужен только набор специфических методов.
/// </summary>
public class EnrollmentRepository : IEnrollmentRepository
{
    private readonly ApplicationDbContext _db;

    /// <summary>
    /// Инициализирует репозиторий, получая контекст БД.
    /// </summary>
    /// <param name="db">Экземпляр <see cref="ApplicationDbContext"/>.</param>
    public EnrollmentRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    /// <inheritdoc />
    public async Task AddAsync(Enrollment enrollment)
    {
        await _db.Enrollments.AddAsync(enrollment);
    }

    /// <inheritdoc />
    public Task<Enrollment?> GetAsync(Guid userId, Guid courseId)
    {
        return _db.Enrollments
            .FirstOrDefaultAsync(x => x.UserId == userId && x.CourseId == courseId);
    }

    /// <inheritdoc />
    public void Remove(Enrollment enrollment)
    {
        _db.Enrollments.Remove(enrollment);
    }
}
