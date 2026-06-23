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

        public EnrollmentController(RequestEnrollmentUseCase requestEnrollmentUseCase, GetAllPendingEnrollments getAllPendingEnrollments,
            ApproveEnrollmentUseCase approveEnrollmentRequest, RejectEnrollmentUseCase rejectEnrollmentUseCase,
            GetAllMyClassesUseCase getAllMyClassesUseCase
            )
        {
            _requestEnrollmentUseCase = requestEnrollmentUseCase;
            _getAllPendingEnrollments = getAllPendingEnrollments;
            _approveEnrollmentRequest = approveEnrollmentRequest;
            _rejectEnrollmentUseCase = rejectEnrollmentUseCase;
            _getAllMyClassesUseCase = getAllMyClassesUseCase;
        }


        [HttpPost]
        [Authorize(Roles = "Student")]
        public async Task<ActionResult<ApiResponse<EnrollmentDTO>>> RequestEnrollment([FromBody] CreateEnrollmentRequest request)
        {
            var enrollment = new CreateEnrollmentDTO(request.ClassId);
            var result = await _requestEnrollmentUseCase.Execute(enrollment);
            return Ok(new ApiResponse<EnrollmentDTO>
            {
                Message = "Enroll Successfully",
                Data = result
            });
        }


        [HttpGet("my-classes")]
        [Authorize(Roles = "Student")]
        public async Task<ActionResult<List<EnrollmentDTO>>> MyClasses([FromQuery] PaginationDTO pagination)
        {
            var approvedEnrollment = await _getAllMyClassesUseCase.Execute(pagination);
            return Ok(approvedEnrollment);
        }



        [HttpGet("pending")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<List<EnrollmentDTO>>> PendingEnrollments([FromQuery] PaginationDTO pagination)
        {
            return Ok(await _getAllPendingEnrollments.Execute(pagination));
        }

        [HttpPost("{enrollmentId}/approve")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<ApiResponse<EnrollmentDTO>>> ApproveEnrollment([FromRoute] Guid enrollmentId)
        {
            var approveEnrollment = await _approveEnrollmentRequest.Execute(enrollmentId);
            return Ok(new ApiResponse<EnrollmentDTO>
            {
                Message = "Approved Successfully",
                Data = approveEnrollment
            });
        }

        [HttpPost("{enrollmentId}/reject")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<ApiResponse<EnrollmentDTO>>> RejectEnrollment([FromRoute] Guid enrollmentId)
        {
            var rejectEnrollment = await _rejectEnrollmentUseCase.Execute(enrollmentId);
            return Ok(new ApiResponse<EnrollmentDTO>
            {
                Message = "Rejected Successfully",
                Data = rejectEnrollment
            });
        }
    }
}
