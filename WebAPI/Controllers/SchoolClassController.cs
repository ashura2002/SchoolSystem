using Application.DTOs;
using Application.Features.Class.Admin.Commands;
using Application.Features.Class.Admin.Queries;
using Application.Features.Class.Teacher.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using WebAPI.Constants;
using WebAPI.DTOs;

namespace WebAPI.Controllers
{

    [ApiController]
    [Route("api/class")]
    [Authorize]
    public class SchoolClassController : ControllerBase
    {
        private readonly IMediator _mediator;

        public SchoolClassController(
            IMediator mediator
            )
        {
            _mediator = mediator;
        }

        // admin
        [HttpPost]
        [Authorize(Roles = Roles.Admin)]
        public async Task<ActionResult<ApiResponse<Guid>>> CreateClass([FromBody] CreateSchoolClassRequest request,
            CancellationToken cancellationToken)
        {
            var command = new CreateSchoolClassCommand(request.Name,
                request.StartTime,
                request.EndTime,
                request.Schedule,
                request.StudentCapacity);

            var result = await _mediator.Send(command, cancellationToken);

            return CreatedAtAction(
                nameof(GetClassById),
                new { id = result },
                new ApiResponse<Guid>
                {
                    Message = "Class created successfully",
                    Data = result
                }
                );
        }


        [HttpPatch("{classId}/assign-teacher")]
        [Authorize(Roles = Roles.Admin)]
        public async Task<ActionResult> AssignTeacher([FromBody] AssignTeacherRequest request,
            [FromRoute] Guid classId, CancellationToken cancellationToken)
        {
            var command = new AssignTeacherCommand(classId, request.TeacherId);
            await _mediator.Send(command, cancellationToken);
            return NoContent();
        }


        [HttpPatch("{classId}/remove-teacher")]
        [Authorize(Roles = Roles.Admin)]
        public async Task<ActionResult> RemoveAssignedTeacher([FromRoute] Guid classId, CancellationToken cancellationToken)
        {
            var command = new RemoveTeacherCommand(classId);
            await _mediator.Send(command, cancellationToken);
            return NoContent();
        }

        [EnableRateLimiting(RateLimitPolicies.GetResources)]
        [HttpGet]
        [Authorize(Roles = $"{Roles.Admin},{Roles.Student},{Roles.Teacher}")]
        public async Task<ActionResult<ApiResponse<IEnumerable<SchoolClassDTO>>>> GetAllClasses(
            [FromQuery] PaginationRequest request,
            CancellationToken cancellationToken)
        {
            var query = new GetAllClassesQuery(request.Page, request.PageSize);
            var result = await _mediator.Send(query, cancellationToken);
            return new ApiResponse<IEnumerable<SchoolClassDTO>>
            {
                Message = "Classes retrieved successfully.",
                Data = result
            };
        }

        [EnableRateLimiting(RateLimitPolicies.GetResources)]
        [HttpGet("without-teacher")]
        [Authorize(Roles = Roles.Admin)]
        public async Task<ActionResult<ApiResponse<IEnumerable<SchoolClassDTO>>>> GetAllClassesWithoutTeacher(
            [FromQuery] PaginationRequest request,
            CancellationToken cancellationToken)
        {

            var query = new GetClassesWithoutTeacherQuery(request.Page, request.PageSize);
            var result = await _mediator.Send(query, cancellationToken);
            return new ApiResponse<IEnumerable<SchoolClassDTO>>
            {
                Message = "Classes retrieved successfully.",
                Data = result
            };
        }

        [EnableRateLimiting(RateLimitPolicies.GetResources)]
        [HttpGet("with-teacher")]
        [Authorize(Roles = Roles.Admin)]
        public async Task<ActionResult<ApiResponse<IEnumerable<SchoolClassDTO>>>> GetAllClassesWithTeacher([FromQuery]
        PaginationRequest request,
            CancellationToken cancellationToken)
        {
            var query = new GetAllClassesWithTeacherQuery(request.Page, request.PageSize);
            var result = await _mediator.Send(query, cancellationToken);
            return new ApiResponse<IEnumerable<SchoolClassDTO>>
            {
                Message = "Classes retrieved successfully.",
                Data = result
            };
        }

        [EnableRateLimiting(RateLimitPolicies.GetResources)]
        [HttpGet("{id}")]
        [Authorize(Roles = Roles.Admin)]
        public async Task<ActionResult<ApiResponse<SchoolClassDTO>>> GetClassById([FromRoute] Guid id,
            CancellationToken cancellationToken)
        {
            var query = new GetClassByIdQuery(id);
            var result = await _mediator.Send(query, cancellationToken);
            return new ApiResponse<SchoolClassDTO>
            {
                Message = "Retrieved successfully.",
                Data = result
            };
        }

        [HttpPatch("{classId}/class-name")]
        [Authorize(Roles = Roles.Admin)]
        public async Task<ActionResult> UpdateClassName([FromBody] UpdateClassNameRequest request,
          [FromRoute] Guid classId, CancellationToken cancellationToken)
        {
            var command = new UpdateClassCommand(classId, request.Name);
            await _mediator.Send(command, cancellationToken);
            return NoContent();
        }

        [HttpDelete("{classId}")]
        [Authorize(Roles = Roles.Admin)]
        public async Task<ActionResult> DeleteClass([FromRoute] Guid classId, CancellationToken cancellationToken)
        {
            var command = new DeleteClassCommand(classId);
            await _mediator.Send(command, cancellationToken);
            return NoContent();
        }


        // teachers
        [EnableRateLimiting(RateLimitPolicies.GetResources)]
        [HttpGet("own-classes")]
        [Authorize(Roles = Roles.Teacher)]
        public async Task<ActionResult<ApiResponse<IEnumerable<SchoolClassDTO>>>> GetAllOwnClasses(
            [FromQuery] PaginationRequest request,
            CancellationToken cancellationToken)
        {
            var query = new GetTeacherOwnClassesQuery(request.Page, request.PageSize);
            var result = await _mediator.Send(query, cancellationToken);
            return new ApiResponse<IEnumerable<SchoolClassDTO>>
            {
                Message = "Classes retrieved successfully.",
                Data = result
            };
        }

        [EnableRateLimiting(RateLimitPolicies.GetResources)]
        [HttpGet("own-classes/{classId}")]
        [Authorize(Roles = Roles.Teacher)]
        public async Task<ActionResult<ApiResponse<TeacherClassDetailDTO>>> GetTeacherClassbyId([FromRoute] Guid classId,
            CancellationToken cancellationToken)
        {
            var query = new GetTeacherClassByIdQuery(classId);
            var result = await _mediator.Send(query, cancellationToken);
            return new ApiResponse<TeacherClassDetailDTO>
            {
                Message = "Class retrieved successfully.",
                Data = result
            };
        }
    }
}