using Application.DTOs;
using Application.Features.Class.Admin.Commands;
using Application.Features.Class.Admin.Queries;
using Application.Features.Class.Teacher.Queries;
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
        private readonly CreateSchoolClassHandler _createSchoolClassHandler;
        private readonly AssignTeacherHandler _assignTeacherHandler;
        private readonly GetAllClassHandler _getAllClassHandler;
        private readonly GetClassesWithoutTeacherHandler _getClassesWithoutTeacherHandler;
        private readonly GetAllClassesWithTeacherHandler _getAllClassesWithTeacherHandler;
        private readonly GetClassByIdHandler _getClassByIdHandler;
        private readonly GetTeacherOwnClassesHandler _getOwnClassesHandler;
        private readonly UpdateClassHandler _updateClassHandler;
        private readonly DeleteClassHandler _deleteClassHandler;
        private readonly GetTeacherClassByIdHandler _getTeacherClassByIdHandler;
        private readonly RemoveTeacherHandler _removeTeacherHandler;

        public SchoolClassController(CreateSchoolClassHandler createSchoolClassHandler, AssignTeacherHandler assignTeacherHandler,
            GetAllClassHandler getAllClassHandler, GetClassesWithoutTeacherHandler getClassesWithoutTeacherHandler,
            GetAllClassesWithTeacherHandler getAllClassesWithTeacherHandler, GetClassByIdHandler getClassByIdHandler,
            GetTeacherOwnClassesHandler getOwnClassesHandler, UpdateClassHandler updateClassHandler,
            DeleteClassHandler deleteClassHandler, GetTeacherClassByIdHandler getTeacherClassByIdHandler,
            RemoveTeacherHandler removeTeacherHandler
            )
        {
            _createSchoolClassHandler = createSchoolClassHandler;
            _assignTeacherHandler = assignTeacherHandler;
            _getAllClassHandler = getAllClassHandler;
            _getClassesWithoutTeacherHandler = getClassesWithoutTeacherHandler;
            _getAllClassesWithTeacherHandler = getAllClassesWithTeacherHandler;
            _getClassByIdHandler = getClassByIdHandler;
            _getOwnClassesHandler = getOwnClassesHandler;
            _updateClassHandler = updateClassHandler;
            _deleteClassHandler = deleteClassHandler;
            _getTeacherClassByIdHandler = getTeacherClassByIdHandler;
            _removeTeacherHandler = removeTeacherHandler;
        }

        // admin
        [HttpPost]
        [Authorize(Roles = Roles.Admin)]
        public async Task<ActionResult<ApiResponse<SchoolClassDTO>>> CreateClass([FromBody] CreateSchoolClassRequest request,
            CancellationToken cancellationToken)
        {
            var command = new CreateSchoolClassCommand(request.Name,request.StartTime, request.EndTime, request.Schedule);
            var result = await _createSchoolClassHandler.Handle(command, cancellationToken);

            return Ok(new ApiResponse<SchoolClassDTO>
            {
                Message = "Class created successfully",
                Data = result
            });
        }


        [HttpPut("{classId}/teacher")]
        [Authorize(Roles = Roles.Admin)]
        public async Task<ActionResult<ApiResponse<SchoolClassDTO>>> AssignTeacher([FromBody] AssignTeacherRequest request,
            [FromRoute] Guid classId, CancellationToken cancellationToken)
        {
            var command = new AssignTeacherCommand(classId, request.TeacherId);
            var result = await _assignTeacherHandler.Handle(command, cancellationToken);

            return Ok(new ApiResponse<SchoolClassDTO>
            {
                Message = "Teacher assigned to class successfully",
                Data = result
            });
        }


        [HttpDelete("{classId}/teacher")]
        [Authorize(Roles = Roles.Admin)]
        public async Task<ActionResult> RemoveAssignedTeacher([FromRoute] Guid classId, CancellationToken cancellationToken)
        {
            var command = new RemoveTeacherCommand(classId);
            await _removeTeacherHandler.Handle(command, cancellationToken);
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
            var result = await _getAllClassHandler.Handle(query, cancellationToken);
            return Ok(new ApiResponse<IEnumerable<SchoolClassDTO>>
            {
                Message = "Classes retrieved successfully.",
                Data = result
            });
        }

        [EnableRateLimiting(RateLimitPolicies.GetResources)]
        [HttpGet("without-teacher")]
        [Authorize(Roles = Roles.Admin)]
        public async Task<ActionResult<ApiResponse<IEnumerable<SchoolClassDTO>>>> GetAllClassesWithoutTeacher(
            [FromQuery] PaginationRequest request,
            CancellationToken cancellationToken)
        {

            var query = new GetClassesWithoutTeacherQuery(request.Page, request.PageSize);
            var result = await _getClassesWithoutTeacherHandler.Handle(query, cancellationToken);
            return Ok(new ApiResponse<IEnumerable<SchoolClassDTO>>
            {
                Message = "Classes retrieved successfully.",
                Data = result
            });
        }

        [EnableRateLimiting(RateLimitPolicies.GetResources)]
        [HttpGet("with-teacher")]
        [Authorize(Roles = Roles.Admin)]
        public async Task<ActionResult<ApiResponse<IEnumerable<SchoolClassDTO>>>> GetAllClassesWithTeacher([FromQuery]
        PaginationRequest request,
            CancellationToken cancellationToken)
        {
            var query = new GetAllClassesWithTeacherQuery(request.Page, request.PageSize);
            var result = await _getAllClassesWithTeacherHandler.Handle(query, cancellationToken);
            return Ok(new ApiResponse<IEnumerable<SchoolClassDTO>>
            {
                Message = "Classes retrieved successfully.",
                Data = result
            });
        }

        [EnableRateLimiting(RateLimitPolicies.GetResources)]
        [HttpGet("{id}")]
        [Authorize(Roles = Roles.Admin)]
        public async Task<ActionResult<ApiResponse<SchoolClassDTO>>> GetClassById([FromRoute] Guid id,
            CancellationToken cancellationToken)
        {
            var query = new GetClassByIdQuery(id);
            var result = await _getClassByIdHandler.Handle(query, cancellationToken);
            return Ok(new ApiResponse<SchoolClassDTO>
            {
                Message = "Retrieved successfully.",
                Data = result
            });
        }

        [HttpPut("{classId}")]
        [Authorize(Roles = Roles.Admin)]
        public async Task<ActionResult<ApiResponse<SchoolClassDTO>>> UpdateClassName([FromBody] UpdateClassNameRequest request,
          [FromRoute] Guid classId, CancellationToken cancellationToken)
        {
            var command = new UpdateClassCommand(classId, request.Name);
            var result = await _updateClassHandler.Handle(command, cancellationToken);
            return Ok(new ApiResponse<SchoolClassDTO>
            {
                Message = "School Class Updated Successfully.",
                Data = result
            });
        }

        [HttpDelete("{classId}")]
        [Authorize(Roles = Roles.Admin)]
        public async Task<ActionResult> DeleteClass([FromRoute] Guid classId, CancellationToken cancellationToken)
        {
            var command = new DeleteClassCommand(classId);
            await _deleteClassHandler.Handle(command, cancellationToken);
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
            var result = await _getOwnClassesHandler.Handle(query, cancellationToken);
            return Ok(new ApiResponse<IEnumerable<SchoolClassDTO>>
            {
                Message = "Classes retrieved successfully.",
                Data = result
            });
        }

        [EnableRateLimiting(RateLimitPolicies.GetResources)]
        [HttpGet("own-classes/{classId}")]
        [Authorize(Roles = Roles.Teacher)]
        public async Task<ActionResult<TeacherClassDetailDTO>> GetTeacherClassbyId([FromRoute] Guid classId,
            CancellationToken cancellationToken)
        {
            var query = new GetTeacherClassByIdQuery(classId);
            return Ok(await _getTeacherClassByIdHandler.Handle(query, cancellationToken));
        }
    }
}