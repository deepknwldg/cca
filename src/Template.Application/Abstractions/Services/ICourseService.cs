using Template.Application.Models.Courses;

namespace Template.Application.Abstractions.Services;

public interface ICourseService
{
    Task<CourseResultDto> CreateAsync(CreateCourseDto dto);
    Task<CourseResultDto?> GetByIdAsync(Guid id);
    Task<IReadOnlyList<CourseResultDto>> GetAllAsync();
    Task<bool> UpdateAsync(Guid id, UpdateCourseDto dto);
    Task<bool> DeleteAsync(Guid id);
}
