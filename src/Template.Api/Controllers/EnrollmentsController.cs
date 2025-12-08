using System.ComponentModel;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using Template.Api.InternalClasses.Routing;
using Template.Api.InternalClasses.Tags;
using Template.Api.Models.Enrollments;
using Template.Application.Abstractions.Services;
using Template.Application.Models.Enrollments;

namespace Template.Api.Controllers;

[ApiController]
[Tags(ApiTags.Enrollments)]
[Route("api/v{version:apiVersion}/")]
public class EnrollmentsController : ControllerBase
{
    private readonly IEnrollmentService _service;

    public EnrollmentsController(IEnrollmentService service)
    {
        _service = service;
    }

    [EndpointSummary("Регистрация на курс")]
    [EndpointName(ApiRouting.Enrollments.Enroll)]
    [HttpPost]
    public async Task<IActionResult> Enroll(
        [Description("Тело запроса"), FromBody] EnrollUserRequest request)
    {
        var dto = request.Adapt<EnrollUserDto>();
        var result = await _service.EnrollAsync(dto);

        return Ok(result);
    }

    [EndpointSummary("Удаление с курс")]
    [EndpointName(ApiRouting.Enrollments.Enroll)]
    [HttpDelete]
    public async Task<IActionResult> Remove(
        [Description("Идентификатор пользователя"), FromQuery] Guid userId,
        [Description("Идентификатор курса"), FromQuery] Guid courseId)
    {
        var deleted = await _service.RemoveAsync(userId, courseId);
        return deleted ? NoContent() : NotFound();
    }
}
