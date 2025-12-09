using Mapster;
using Template.Application.Abstractions.Persistence.Repositories;
using Template.Application.Abstractions.Services;
using Template.Application.Models.Lessons;
using Template.Domain.Entities;

namespace Template.Application.Services;

public class LessonService : ILessonService
{
    private readonly IServiceExecutor _executor;
    private readonly ILessonRepository _repo;
    private readonly IUnitOfWork _uow;

    public LessonService(
        IServiceExecutor executor,
        ILessonRepository repo,
        IUnitOfWork uow)
    {
        _executor = executor;
        _repo = repo;
        _uow = uow;
    }

    public async Task<LessonResultDto> CreateAsync(CreateLessonDto dto)
    {
        await _uow.BeginTransactionAsync();
        try
        {
            var entity = dto.Adapt<Lesson>();

            await _repo.AddAsync(entity);
            await _uow.SaveChangesAsync();

            await _uow.CommitAsync();
            return entity.Adapt<LessonResultDto>();
        }
        catch
        {
            await _uow.RollbackAsync();
            throw;
        }
    }

    public async Task<LessonResultDto?> GetByIdAsync(Guid id)
    {
        var entity = await _repo.GetByIdAsync(id);
        return entity?.Adapt<LessonResultDto>();
    }

    public async Task<IReadOnlyList<LessonResultDto>> GetByCourseAsync(Guid courseId)
    {
        var items = await _repo.GetByCourseIdAsync(courseId);
        return items.Adapt<IReadOnlyList<LessonResultDto>>();
    }

    public async Task<bool> UpdateAsync(Guid id, UpdateLessonDto dto)
    {
        await _uow.BeginTransactionAsync();
        try
        {
            var entity = await _repo.GetByIdAsync(id);
            if (entity == null) return false;

            dto.Adapt(entity);

            _repo.Update(entity);
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

    public async Task<bool> DeleteAsync(Guid id)
    {
        await _uow.BeginTransactionAsync();
        try
        {
            var entity = await _repo.GetByIdAsync(id);
            if (entity == null) return false;

            _repo.Remove(entity);
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
