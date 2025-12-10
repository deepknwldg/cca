using Template.Application.Models.Common;
using Template.Application.Models.Users;
using Template.Domain.ValueObjects;

namespace Template.Application.Abstractions.Services;

public interface IUserService
{
    Task<UserResultDto> CreateAsync(CreateUserDto dto, CancellationToken cancellationToken = default);
    Task<UserResultDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<PagedResult<UserResultDto>> GetAllAsync(PagingParams paging, CancellationToken cancellationToken = default);
    Task<bool> UpdateAsync(Guid id, UpdateUserDto dto, CancellationToken cancellationToken = default);
    Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}
