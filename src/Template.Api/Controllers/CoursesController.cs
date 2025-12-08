using System.ComponentModel;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using Template.Api.InternalClasses.Routing;
using Template.Api.InternalClasses.Tags;
using Template.Api.Models.Course;
using Template.Application.Abstractions.Services;
using Template.Application.Models.Courses;

namespace Template.Api.Controllers;

[ApiController]
[Tags(ApiTags.Courses)]
[Route("api")]
public class CoursesController : ControllerBase
{
    private readonly ICourseService _service;

    public CoursesController(ICourseService service)
    {
        _service = service;
    }

    [EndpointSummary("Создание курса")]
    [EndpointName(ApiRouting.Courses.Create)]
    [HttpPost(ApiRouting.Courses.Create)]
    public async Task<IActionResult> Create(
        [Description("Тело запроса"), FromBody] CreateCourseRequest request)
    {
        var dto = request.Adapt<CreateCourseDto>();
        var result = await _service.CreateAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }

    [EndpointSummary("Получение курса по идентификатору")]
    [EndpointName(ApiRouting.Courses.GetById)]
    [HttpGet(ApiRouting.Courses.GetById)]
    public async Task<IActionResult> GetById(
        [Description("Идентификатор курса"), FromRoute] Guid id)
    {
        var result = await _service.GetByIdAsync(id);
        return result is null ? NotFound() : Ok(result);
    }

    [EndpointSummary("Получение всех курсов")]
    [EndpointName(ApiRouting.Courses.GetAll)]
    [HttpGet(ApiRouting.Courses.GetAll)]
    public async Task<IActionResult> GetAll()
    {
        var result = await _service.GetAllAsync();
        return Ok(result);
    }

    [EndpointSummary("Обновление курса")]
    [EndpointName(ApiRouting.Courses.Update)]
    [HttpPut(ApiRouting.Courses.Update)]
    public async Task<IActionResult> Update(
        [Description("Идентификатор курса"), FromRoute] Guid id,
        [Description("Тело запроса"), FromBody] UpdateCourseRequest request)
    {
        var dto = request.Adapt<UpdateCourseDto>();
        var success = await _service.UpdateAsync(id, dto);
        return success ? NoContent() : NotFound();
    }

    [EndpointSummary("Удаление курса")]
    [EndpointName(ApiRouting.Courses.Delete)]
    [HttpDelete(ApiRouting.Courses.Delete)]
    public async Task<IActionResult> Delete(
        [Description("Идентификатор курса"), FromRoute] Guid id)
    {
        var success = await _service.DeleteAsync(id);
        return success ? NoContent() : NotFound();
    }
}
