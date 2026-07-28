using Application.DTOs;
using Application.Features.Enrollments.Admin.Commands;
using Application.Features.Enrollments.Admin.Queries;
using Application.Features.Enrollments.Student.Commands;
using Application.Features.Enrollments.Student.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using WebAPI.Constants;
using WebAPI.DTOs;

namespace WebAPI.Controllers
{

    [ApiController]
    [Route("api/enrollment")]
    [Authorize]
    public class EnrollmentController : ControllerBase
    {
        private readonly IMediator _mediator;
    
        public EnrollmentController(
            IMediator mediator
            )
        {
            _mediator = mediator;
        }


        [HttpPost]
        [Authorize(Roles = Roles.Student)]
        public async Task<ActionResult<ApiResponse<Guid>>> RequestEnrollment([FromBody] CreateEnrollmentRequest request,
            CancellationToken cancellationToken)
        {
            var command = new RequestEnrollmentCommand(request.ClassId);
            var result = await _mediator.Send(command, cancellationToken);
            return CreatedAtAction(
                nameof(MyClassesById),
                new { id = result },
                new ApiResponse<Guid>
                {
                    Message = "Enroll Successfully",
                    Data = result
                }
                );
        }

        [EnableRateLimiting(RateLimitPolicies.GetResources)]
        [HttpGet("my-classes")]
        [Authorize(Roles = Roles.Student)]
        public async Task<ActionResult<ApiResponse<IEnumerable<EnrollmentResponseDTO>>>> MyClasses(
            [FromQuery] PaginationRequest request,
            CancellationToken cancellationToken)
        {
            var query = new GetAllMyClassesQuery(request.Page, request.PageSize);
            var result = await _mediator.Send(query, cancellationToken);
            return new ApiResponse<IEnumerable<EnrollmentResponseDTO>>
            {
                Message = "Enrollments retrieved successfully",
                Data = result
            };
        }

        [EnableRateLimiting(RateLimitPolicies.GetResources)]
        [HttpGet("my-classes/{enrollmentId}")]
        [Authorize(Roles = Roles.Student)]
        public async Task<ActionResult<ApiResponse<EnrollmentDetailsDTO>>> MyClassesById(
         [FromRoute] Guid enrollmentId,
         CancellationToken cancellationToken)
        {
            var query = new GetMyClassByIdQuery(enrollmentId);
            var result = await _mediator.Send(query, cancellationToken);
            return new ApiResponse<EnrollmentDetailsDTO>
            {
                Message = "Enrollment retrieved successfully",
                Data = result
            };
        }

        [HttpPatch("{enrollmentId}/cancel")]
        [Authorize(Roles = Roles.Student)]
        public async Task<ActionResult> CancelEnrollment([FromRoute] Guid enrollmentId,
            CancellationToken cancellationToken)
        {
            var command = new CancelEnrollmentCommand(enrollmentId);
            await _mediator.Send(command, cancellationToken);
            return NoContent();
        }

        [HttpDelete("my-classes/{enrollmentId}")]
        [Authorize(Roles = Roles.Student)]
        public async Task<ActionResult> DropEnrollment([FromRoute] Guid enrollmentId,
            CancellationToken cancellationToken)
        {
            var command = new DropEnrollmentCommand(enrollmentId);
            await _mediator.Send(command, cancellationToken);
            return NoContent();
        }

        [EnableRateLimiting(RateLimitPolicies.GetResources)]
        [HttpGet("pending")]
        [Authorize(Roles = Roles.Admin)]
        public async Task<ActionResult<ApiResponse<IEnumerable<PendingEnrollmentResponseDTO>>>> PendingEnrollments(
            [FromQuery] PaginationRequest request,
            CancellationToken cancellationToken)
        {
            var query = new GetAllPendingEnrollmentQuery(request.Page, request.PageSize);
            var result = await _mediator.Send(query, cancellationToken);
            return new ApiResponse<IEnumerable<PendingEnrollmentResponseDTO>>
            {
                Message = "Enrollment retrieved successfully",
                Data = result
            };
        }

        [HttpPatch("{enrollmentId}/approve")]
        [Authorize(Roles = Roles.Admin)]
        public async Task<ActionResult> ApproveEnrollment([FromRoute] Guid enrollmentId,
            CancellationToken cancellationToken)
        {
            var command = new ApprovedEnrollmentCommand(enrollmentId);
            await _mediator.Send(command, cancellationToken);
            return NoContent();
        }

        [HttpPatch("{enrollmentId}/reject")]
        [Authorize(Roles = Roles.Admin)]
        public async Task<ActionResult> RejectEnrollment([FromRoute] Guid enrollmentId,
            CancellationToken cancellationToken)
        {
            var command = new RejectEnrollmentCommand(enrollmentId);
            await _mediator.Send(command, cancellationToken);
            return NoContent();
        }
    }
}
