using Application.DTOs;
using Application.Features.Enrollments.Admin.Commands;
using Application.Features.Enrollments.Admin.Queries;
using Application.Features.Enrollments.Student.Commands;
using Application.Features.Enrollments.Student.Queries;
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
        private readonly RequestEnrollmentHandler _requestEnrollmentHandler;
        private readonly GetAllPendingEnrollmentsHandler _getAllPendingEnrollmentsHandler;
        private readonly ApproveEnrollmentHandler _approveEnrollmentRequestHandler;
        private readonly RejectEnrollmentHandler _rejectEnrollmentHandler;
        private readonly GetAllMyClassesHandler _getAllMyClassesHandler;
        private readonly CancelEnrollmentHandler _cancelEnrollmentHandler;
        private readonly DropEnrollmentHandler _dropEnrollmentHandler;
        private readonly GetMyClassByIdhandler _getMyClassByIdhandler;

        public EnrollmentController(RequestEnrollmentHandler requestEnrollmentHandler,
            GetAllPendingEnrollmentsHandler getAllPendingEnrollmentsHandler,
            ApproveEnrollmentHandler approveEnrollmentRequestHandler, RejectEnrollmentHandler rejectEnrollmentHandler,
            GetAllMyClassesHandler getAllMyClassesHandler, CancelEnrollmentHandler cancelEnrollmentHandler,
            DropEnrollmentHandler dropEnrollmentHandler, GetMyClassByIdhandler getMyClassByIdhandler
            )
        {
            _requestEnrollmentHandler = requestEnrollmentHandler;
            _getAllPendingEnrollmentsHandler = getAllPendingEnrollmentsHandler;
            _approveEnrollmentRequestHandler = approveEnrollmentRequestHandler;
            _rejectEnrollmentHandler = rejectEnrollmentHandler;
            _getAllMyClassesHandler = getAllMyClassesHandler;
            _cancelEnrollmentHandler = cancelEnrollmentHandler;
            _dropEnrollmentHandler = dropEnrollmentHandler;
            _getMyClassByIdhandler = getMyClassByIdhandler;
        }


        [HttpPost]
        [Authorize(Roles = Roles.Student)]
        public async Task<ActionResult<ApiResponse<Guid>>> RequestEnrollment([FromBody] CreateEnrollmentRequest request,
            CancellationToken cancellationToken)
        {
            var enrollment = new RequestEnrollmentCommand(request.ClassId);
            var result = await _requestEnrollmentHandler.Handle(enrollment, cancellationToken);
            return StatusCode(StatusCodes.Status201Created, new ApiResponse<Guid>
            {
                Message = "Enroll Successfully",
                Data = result
            });
        }

        [EnableRateLimiting(RateLimitPolicies.GetResources)]
        [HttpGet("my-classes")]
        [Authorize(Roles = Roles.Student)]
        public async Task<ActionResult<ApiResponse<IEnumerable<EnrollmentResponseDTO>>>> MyClasses(
            [FromQuery] PaginationRequest request,
            CancellationToken cancellationToken)
        {
            var query = new GetAllMyClassesQuery(request.Page, request.PageSize);
            var result = await _getAllMyClassesHandler.Handle(query, cancellationToken);
            return Ok(new ApiResponse<IEnumerable<EnrollmentResponseDTO>>
            {
                Message = "Enrollments retrieved successfully",
                Data = result
            });
        }

        [EnableRateLimiting(RateLimitPolicies.GetResources)]
        [HttpGet("my-classes/{enrollmentId}")]
        [Authorize(Roles = Roles.Student)]
        public async Task<ActionResult<ApiResponse<EnrollmentDetailsDTO>>> MyClassesById(
         [FromRoute] Guid enrollmentId,
         CancellationToken cancellationToken)
        {
            var query = new GetMyClassByIdQuery(enrollmentId);
            var result = await _getMyClassByIdhandler.Handle(query, cancellationToken);
            return Ok(new ApiResponse<EnrollmentDetailsDTO>
            {
                Message = "Enrollment retrieved successfully",
                Data = result
            });
        }

        [HttpPatch("{enrollmentId}/cancel")]
        [Authorize(Roles = Roles.Student)]
        public async Task<IActionResult> CancelEnrollment([FromRoute] Guid enrollmentId,
            CancellationToken cancellationToken)
        {
            var command = new CancelEnrollmentCommand(enrollmentId);
            await _cancelEnrollmentHandler.Handle(command, cancellationToken);
            return NoContent();
        }

        [HttpDelete("my-classes/{enrollmentId}")]
        [Authorize(Roles = Roles.Student)]
        public async Task<IActionResult> DropEnrollment([FromRoute] Guid enrollmentId,
            CancellationToken cancellationToken)
        {
            var command = new DropEnrollmentCommand(enrollmentId);
            await _dropEnrollmentHandler.Handle(command, cancellationToken);
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
            var result = await _getAllPendingEnrollmentsHandler.Handle(query, cancellationToken);
            return Ok(new ApiResponse<IEnumerable<PendingEnrollmentResponseDTO>>
            {
                Message = "Enrollment retrieved successfully",
                Data = result
            });
        }

        [HttpPatch("{enrollmentId}/approve")]
        [Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> ApproveEnrollment([FromRoute] Guid enrollmentId,
            CancellationToken cancellationToken)
        {
            var command = new ApprovedEnrollmentCommand(enrollmentId);
            await _approveEnrollmentRequestHandler.Handle(command, cancellationToken);
            return NoContent();
        }

        [HttpPatch("{enrollmentId}/reject")]
        [Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> RejectEnrollment([FromRoute] Guid enrollmentId,
            CancellationToken cancellationToken)
        {
            var command = new RejectEnrollmentCommand(enrollmentId);
            await _rejectEnrollmentHandler.Handle(command, cancellationToken);
            return NoContent();
        }
    }
}
