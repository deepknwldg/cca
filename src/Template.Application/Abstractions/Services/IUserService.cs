using Template.Application.Models.Users;

namespace Template.Application.Abstractions.Services;

public interface IUserService
{
    Task<UserResultDto> CreateAsync(CreateUserDto dto);
    Task<UserResultDto?> GetByIdAsync(Guid id);
    Task<IReadOnlyList<UserResultDto>> GetAllAsync();
    Task<bool> UpdateAsync(Guid id, UpdateUserDto dto);
    Task<bool> DeleteAsync(Guid id);
}
