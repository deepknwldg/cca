using System.ComponentModel;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using Template.Api.InternalClasses.Routing;
using Template.Api.InternalClasses.Tags;
using Template.Api.Models.User;
using Template.Application.Abstractions.Services;
using Template.Application.Models.Users;

namespace Template.Api.Controllers;

[ApiController]
[Tags(ApiTags.Users)]
[Route("api")]
public class UsersController : ControllerBase
{
    private readonly IUserService _service;

    public UsersController(IUserService service)
    {
        _service = service;
    }

    [EndpointSummary("Создание пользователя")]
    [EndpointName(ApiRouting.Users.Create)]
    [HttpPost(ApiRouting.Users.Create)]
    public async Task<IActionResult> Create(
        [Description("Тело запроса"), FromBody] CreateUserRequest request)
    {
        var dto = request.Adapt<CreateUserDto>();
        var result = await _service.CreateAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }

    [EndpointSummary("Получение пользователя по идентификатору")]
    [EndpointName(ApiRouting.Users.GetById)]
    [HttpGet(ApiRouting.Users.GetById)]
    public async Task<IActionResult> GetById(
        [Description("Идентификатор пользователя"), FromRoute] Guid id)
    {
        var user = await _service.GetByIdAsync(id);
        return user is null ? NotFound() : Ok(user);
    }

    [EndpointSummary("Получение всех пользователей")]
    [EndpointName(ApiRouting.Users.GetAll)]
    [HttpGet(ApiRouting.Users.GetAll)]
    public async Task<IActionResult> GetAll()
    {
        var items = await _service.GetAllAsync();
        return Ok(items);
    }

    [EndpointSummary("Обновление пользователя")]
    [EndpointName(ApiRouting.Users.Update)]
    [HttpPut(ApiRouting.Users.Update)]
    public async Task<IActionResult> Update(
        [Description("Идентификатор пользователя"), FromRoute] Guid id,
        [Description("Тело запроса"), FromBody] UpdateUserRequest request)
    {
        var dto = request.Adapt<UpdateUserDto>();
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
