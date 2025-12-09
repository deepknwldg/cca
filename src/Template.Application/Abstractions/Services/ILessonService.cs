using Template.Application.Models.Lessons;

namespace Template.Application.Abstractions.Services;

public interface ILessonService
{
    Task<LessonResultDto> CreateAsync(CreateLessonDto dto);
    Task<bool> DeleteAsync(Guid id);
    Task<IReadOnlyList<LessonResultDto>> GetByCourseAsync(Guid courseId);
    Task<LessonResultDto?> GetByIdAsync(Guid id);
    Task<bool> UpdateAsync(Guid id, UpdateLessonDto dto);
}