using Application.DTOs;
using Application.UseCases.Class;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAPI.DTOs;

namespace WebAPI.Controllers
{

    [ApiController]
    [Route("api/class")]
    public class SchoolClassController : ControllerBase
    {
        private readonly CreateSchoolClassUseCase _createSchoolClassUseCase;
        private readonly AssignTeacherUseCase _assignTeacherUseCase;
        private readonly GetAllClassUseCase _getAllClassUseCase;
        private readonly GetClassesWithoutTeacher _getClassesWithoutTeacher;
        private readonly GetAllClassesWithTeacher _getAllClassesWithTeacher;

        public SchoolClassController(CreateSchoolClassUseCase createSchoolClassUseCase, AssignTeacherUseCase assignTeacherUseCase,
            GetAllClassUseCase getAllClassUseCase, GetClassesWithoutTeacher getClassesWithoutTeacher,
            GetAllClassesWithTeacher getAllClassesWithTeacher
            )
        {
            _createSchoolClassUseCase = createSchoolClassUseCase;
            _assignTeacherUseCase = assignTeacherUseCase;
            _getAllClassUseCase = getAllClassUseCase;
            _getClassesWithoutTeacher = getClassesWithoutTeacher;
            _getAllClassesWithTeacher = getAllClassesWithTeacher;
        }


        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<ApiResponse<SchoolClassDTO>>> CreateClass([FromBody] CreateSchoolClassRequest request)
        {
            var newClass = new CreateClassDTO(request.Name);
            var result = await _createSchoolClassUseCase.Execute(newClass);

            return Ok(new ApiResponse<SchoolClassDTO>
            {
                Message = "Class created successfully",
                Data = result
            });
        }


        [HttpPut("teacher-assignment/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<ApiResponse<SchoolClassDTO>>> AssignTeacher([FromBody] AssignTeacherRequest request,
            [FromRoute] Guid id)
        {
            var teacher = new AssignTeacherDTO(request.TeacherId);
            var result = await _assignTeacherUseCase.Execute(teacher, id);

            return Ok(new ApiResponse<SchoolClassDTO>
            {
                Message = "Teacher Assign Successfully",
                Data = result
            });
        }


        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<List<SchoolClassDTO>>> GetAllClasses([FromRoute] PaginationDTO pagination)
        {
            return Ok(await _getAllClassUseCase.Execute(pagination));
        }

        [HttpGet("without-teacher")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<List<SchoolClassDTO>>> GetAllClassesWithoutATeacher([FromRoute] PaginationDTO pagination)
        {
            return Ok(await _getClassesWithoutTeacher.Execute(pagination));
        }

        [HttpGet("with-teacher")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<List<SchoolClassDTO>>> GetAllClassesWithATeacher([FromRoute] PaginationDTO pagination)
        {
            return Ok(await _getAllClassesWithTeacher.Execute(pagination));
        }
    }
}

// get class by id
// get  unassigned teacher