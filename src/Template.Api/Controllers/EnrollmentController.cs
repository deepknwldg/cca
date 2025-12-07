using Mapster;
using Microsoft.AspNetCore.Mvc;
using Template.Api.Models.Enrollments;
using Template.Application.Abstractions.Services;
using Template.Application.Models.Enrollments;

namespace Template.Api.Controllers
{
    [ApiController]
    [Route("api/enrollments")]
    public class EnrollmentController : ControllerBase
    {
        private readonly IEnrollmentService _service;

        public EnrollmentController(IEnrollmentService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> Enroll(EnrollUserRequest request)
        {
            var dto = request.Adapt<EnrollUserDto>();
            var result = await _service.EnrollAsync(dto);

            return Ok(result);
        }

        [HttpDelete]
        public async Task<IActionResult> Remove([FromQuery] Guid userId, [FromQuery] Guid courseId)
        {
            var deleted = await _service.RemoveAsync(userId, courseId);
            return deleted ? NoContent() : NotFound();
        }
    }
}
