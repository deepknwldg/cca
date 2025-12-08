using System.ComponentModel;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using Template.Api.InternalClasses.Routing;
using Template.Api.InternalClasses.Tags;
using Template.Api.Models.Lesson;
using Template.Application.Abstractions.Services;
using Template.Application.Models.Lesson;

namespace Template.Api.Controllers;

[ApiController]
[Tags(ApiTags.Lessons)]
[Route("api")]
public class LessonsController : ControllerBase
{
    private readonly ILessonService _service;

    public LessonsController(ILessonService service)
    {
        _service = service;
    }

    [EndpointSummary("Создание урока")]
    [EndpointName(ApiRouting.Lessons.Create)]
    [HttpPost(ApiRouting.Lessons.Create)]
    public async Task<IActionResult> Create(
        [Description("Тело запроса"), FromBody] CreateLessonRequest request)
    {
        var dto = request.Adapt<CreateLessonDto>();
        var result = await _service.CreateAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }

    [EndpointSummary("Получение урока по идентификатору")]
    [EndpointName(ApiRouting.Lessons.GetById)]
    [HttpGet(ApiRouting.Lessons.GetById)]
    public async Task<IActionResult> GetById(
        [Description("Идентификатор урока"), FromRoute] Guid id)
    {
        var lesson = await _service.GetByIdAsync(id);
        return lesson is null ? NotFound() : Ok(lesson);
    }

    [EndpointSummary("Получение урока по идентификатору курса")]
    [EndpointName(ApiRouting.Lessons.GetByCourse)]
    [HttpGet(ApiRouting.Lessons.GetByCourse)]
    public async Task<IActionResult> GetByCourse(
        [Description("Идентификатор курса"), FromRoute] Guid courseId)
    {
        var items = await _service.GetByCourseAsync(courseId);
        return Ok(items);
    }

    [EndpointSummary("Обновление урока")]
    [EndpointName(ApiRouting.Lessons.Update)]
    [HttpPut(ApiRouting.Lessons.Update)]
    public async Task<IActionResult> Update(
         [Description("Идентификатор курса"), FromRoute] Guid id,
         [Description("Тело запроса"), FromBody] UpdateLessonRequest request)
    {
        var dto = request.Adapt<UpdateLessonDto>();
        var updated = await _service.UpdateAsync(id, dto);
        return updated ? NoContent() : NotFound();
    }

    [EndpointSummary("Удаление урока")]
    [EndpointName(ApiRouting.Lessons.Delete)]
    [HttpDelete(ApiRouting.Lessons.Update)]
    public async Task<IActionResult> Delete(
         [Description("Идентификатор курса"), FromRoute] Guid id)
    {
        var deleted = await _service.DeleteAsync(id);
        return deleted ? NoContent() : NotFound();
    }
}
