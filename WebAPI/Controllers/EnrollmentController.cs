using Application.DTOs;
using Application.UseCases.Enrollments.Admin;
using Application.UseCases.Enrollments.Student;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAPI.DTOs;

namespace WebAPI.Controllers
{

    [ApiController]
    [Route("api/enrollment")]
    public class EnrollmentController : ControllerBase
    {
        private readonly RequestEnrollmentUseCase _requestEnrollmentUseCase;
        private readonly GetAllPendingEnrollments _getAllPendingEnrollments;
        private readonly ApproveEnrollmentUseCase _approveEnrollmentRequest;
        private readonly RejectEnrollmentUseCase _rejectEnrollmentUseCase;
        private readonly GetAllMyClassesUseCase _getAllMyClassesUseCase;
        private readonly CancelEnrollmentUseCase _cancelEnrollmentUseCas;
        private readonly DropEnrollmentUseCase _dropEnrollmentUseCase;

        public EnrollmentController(RequestEnrollmentUseCase requestEnrollmentUseCase, GetAllPendingEnrollments getAllPendingEnrollments,
            ApproveEnrollmentUseCase approveEnrollmentRequest, RejectEnrollmentUseCase rejectEnrollmentUseCase,
            GetAllMyClassesUseCase getAllMyClassesUseCase, CancelEnrollmentUseCase cancelEnrollmentUseCase,
            DropEnrollmentUseCase dropEnrollmentUseCase
            )
        {
            _requestEnrollmentUseCase = requestEnrollmentUseCase;
            _getAllPendingEnrollments = getAllPendingEnrollments;
            _approveEnrollmentRequest = approveEnrollmentRequest;
            _rejectEnrollmentUseCase = rejectEnrollmentUseCase;
            _getAllMyClassesUseCase = getAllMyClassesUseCase;
            _cancelEnrollmentUseCas = cancelEnrollmentUseCase;
            _dropEnrollmentUseCase = dropEnrollmentUseCase;
        }


        [HttpPost]
        [Authorize(Roles = "Student")]
        public async Task<ActionResult<ApiResponse<EnrollmentDTO>>> RequestEnrollment([FromBody] CreateEnrollmentRequest request,
            CancellationToken cancellationToken)
        {
            var enrollment = new CreateEnrollmentDTO(request.ClassId);
            var result = await _requestEnrollmentUseCase.Execute(enrollment, cancellationToken);
            return Ok(new ApiResponse<EnrollmentDTO>
            {
                Message = "Enroll Successfully",
                Data = result
            });
        }

        [HttpGet("my-classes")]
        [Authorize(Roles = "Student")]
        public async Task<ActionResult<List<EnrollmentDTO>>> MyClasses([FromQuery] PaginationDTO pagination,
            CancellationToken cancellationToken)
        {
            var approvedEnrollment = await _getAllMyClassesUseCase.Execute(pagination, cancellationToken);
            return Ok(approvedEnrollment);
        }

        [HttpPut("requests/{enrollmentId}")]
        [Authorize(Roles = "Student")]
        public async Task<ActionResult<ApiResponse<EnrollmentDTO>>> CancelEnrollment([FromRoute] Guid enrollmentId,
            CancellationToken cancellationToken)
        {
            var enrollment = await _cancelEnrollmentUseCas.Execute(enrollmentId, cancellationToken);
            return Ok(new ApiResponse<EnrollmentDTO>
            {
                Message = "Cancel successfully",
                Data = enrollment
            });
        }

        [HttpDelete("my-classes/{enrollmentId}")]
        [Authorize(Roles = "Student")]
        public async Task<ActionResult<ApiResponse<EnrollmentDTO>>> DropEnrollment([FromRoute] Guid enrollmentId,
            CancellationToken cancellationToken)
        {
            var enrollment = await _dropEnrollmentUseCase.Execute(enrollmentId, cancellationToken);
            return Ok(new ApiResponse<EnrollmentDTO>
            {
                Message = "Drop successfully",
                Data = enrollment
            });
        }

        [HttpGet("pending")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<List<EnrollmentDTO>>> PendingEnrollments([FromQuery] PaginationDTO pagination,
            CancellationToken cancellationToken)
        {
            return Ok(await _getAllPendingEnrollments.Execute(pagination, cancellationToken));
        }

        [HttpPost("{enrollmentId}/approve")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<ApiResponse<EnrollmentDTO>>> ApproveEnrollment([FromRoute] Guid enrollmentId,
            CancellationToken cancellationToken)
        {
            var approveEnrollment = await _approveEnrollmentRequest.Execute(enrollmentId, cancellationToken);
            return Ok(new ApiResponse<EnrollmentDTO>
            {
                Message = "Approved Successfully",
                Data = approveEnrollment
            });
        }

        [HttpPost("{enrollmentId}/reject")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<ApiResponse<EnrollmentDTO>>> RejectEnrollment([FromRoute] Guid enrollmentId,
            CancellationToken cancellationToken)
        {
            var rejectEnrollment = await _rejectEnrollmentUseCase.Execute(enrollmentId, cancellationToken);
            return Ok(new ApiResponse<EnrollmentDTO>
            {
                Message = "Rejected Successfully",
                Data = rejectEnrollment
            });
        }
    }
}
