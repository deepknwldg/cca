using System.ComponentModel;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Template.Api.InternalClasses.Routing;
using Template.Api.InternalClasses.Tags;
using Template.Api.Models.Common;
using Template.Api.Models.User;
using Template.Application.Abstractions.Services;
using Template.Application.Models.Users;
using Template.Domain.ValueObjects;

namespace Template.Api.Controllers;

[ApiController]
[Tags(ApiTags.Users)]
[Route("api")]
public class UsersController : ControllerBase
{
    private readonly IUserService _service;
    private readonly IMapper _mapper;

    public UsersController(
        IUserService service,
        IMapper mapper)
    {
        _service = service;
        _mapper = mapper;
    }

    [EndpointSummary("Создание пользователя")]
    [EndpointName(ApiRouting.Users.Create)]
    [HttpPost(ApiRouting.Users.Create)]
    public async Task<IActionResult> Create(
        [Description("Тело запроса"), FromBody] CreateUserRequest request,
        CancellationToken cancellationToken)
    {
        var dto = _mapper.Map<CreateUserDto>(request);
        var result = await _service.CreateAsync(dto, cancellationToken);
        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }

    [EndpointSummary("Получение пользователя по идентификатору")]
    [EndpointName(ApiRouting.Users.GetById)]
    [HttpGet(ApiRouting.Users.GetById)]
    public async Task<IActionResult> GetById(
        [Description("Идентификатор пользователя"), FromRoute] Guid id,
        CancellationToken cancellationToken)
    {
        var user = await _service.GetByIdAsync(id, cancellationToken);
        return user is null ? NotFound() : Ok(user);
    }

    [EndpointSummary("Получение всех пользователей")]
    [EndpointName(ApiRouting.Users.GetAll)]
    [HttpGet(ApiRouting.Users.GetAll)]
    public async Task<IActionResult> GetAll(
        [Description("Параметры для пагинации")][FromQuery] PagedRequest request,
        CancellationToken cancellationToken)
    {
        var paging = _mapper.Map<PagingParams>(request);
        var items = await _service.GetAllAsync(paging, cancellationToken);
        return Ok(items);
    }

    [EndpointSummary("Обновление пользователя")]
    [EndpointName(ApiRouting.Users.Update)]
    [HttpPut(ApiRouting.Users.Update)]
    public async Task<IActionResult> Update(
        [Description("Идентификатор пользователя"), FromRoute] Guid id,
        [Description("Тело запроса"), FromBody] UpdateUserRequest request)
    {
        var dto = _mapper.Map<UpdateUserDto>(request);
        var updated = await _service.UpdateAsync(id, dto);
        return updated ? NoContent() : NotFound();
    }

    [EndpointSummary("Удаление пользователя")]
    [EndpointName(ApiRouting.Users.Delete)]
    [HttpDelete(ApiRouting.Users.Delete)]
    public async Task<IActionResult> Delete(
        [Description("Идентификатор пользователя"), FromRoute] Guid id)
    {
        var deleted = await _service.DeleteAsync(id);
        return deleted ? NoContent() : NotFound();
    }
}
