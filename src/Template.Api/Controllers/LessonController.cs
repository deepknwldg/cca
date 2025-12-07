using Mapster;
using Microsoft.AspNetCore.Mvc;
using Template.Api.Models.Lesson;
using Template.Application.Abstractions.Services;
using Template.Application.Models.Lesson;

namespace Template.Api.Controllers;

[ApiController]
[Route("api/lessons")]
public class LessonController : ControllerBase
{
    private readonly ILessonService _service;

    public LessonController(ILessonService service)
    {
        _service = service;
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateLessonRequest request)
    {
        var dto = request.Adapt<CreateLessonDto>();
        var result = await _service.CreateAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var lesson = await _service.GetByIdAsync(id);
        return lesson is null ? NotFound() : Ok(lesson);
    }

    [HttpGet("by-course/{courseId:guid}")]
    public async Task<IActionResult> GetByCourse(Guid courseId)
    {
        var items = await _service.GetByCourseAsync(courseId);
        return Ok(items);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, UpdateLessonRequest request)
    {
        var dto = request.Adapt<UpdateLessonDto>();
        var updated = await _service.UpdateAsync(id, dto);
        return updated ? NoContent() : NotFound();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var deleted = await _service.DeleteAsync(id);
        return deleted ? NoContent() : NotFound();
    }
}
