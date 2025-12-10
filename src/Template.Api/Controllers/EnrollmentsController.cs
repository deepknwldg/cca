using System.ComponentModel;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Template.Api.InternalClasses.Routing;
using Template.Api.InternalClasses.Tags;
using Template.Api.Models.Enrollments;
using Template.Application.Abstractions.Services;
using Template.Application.Models.Enrollments;

namespace Template.Api.Controllers;

[ApiController]
[Tags(ApiTags.Enrollments)]
[Route("api")]
public class EnrollmentsController : ControllerBase
{
    private readonly IEnrollmentService _service;
    private readonly IMapper _mapper;

    public EnrollmentsController(
        IEnrollmentService service,
        IMapper mapper)
    {
        _service = service;
        _mapper = mapper;
    }

    [EndpointSummary("Регистрация на курс")]
    [EndpointName(ApiRouting.Enrollments.Enroll)]
    [HttpPost(ApiRouting.Enrollments.Enroll)]
    public async Task<IActionResult> Enroll(
        [Description("Тело запроса"), FromBody] EnrollUserRequest request)
    {
        var dto = _mapper.Map<EnrollUserDto>(request);
        var result = await _service.EnrollAsync(dto);

        return Ok(result);
    }

    [EndpointSummary("Удаление с курса")]
    [EndpointName(ApiRouting.Enrollments.Remove)]
    [HttpDelete(ApiRouting.Enrollments.Remove)]
    public async Task<IActionResult> Remove(
        [Description("Идентификатор пользователя"), FromRoute] Guid userId,
        [Description("Идентификатор курса"), FromRoute] Guid courseId)
    {
        var deleted = await _service.RemoveAsync(userId, courseId);
        return deleted ? NoContent() : NotFound();
    }
}
