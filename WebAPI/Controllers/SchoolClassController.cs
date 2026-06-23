using Application.DTOs;
using Application.UseCases.Class.Admin;
using Application.UseCases.Class.Teacher;
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
        private readonly GetClassByIdUseCase _getClassByIdUseCase;
        private readonly GetTeacherOwnClasses _getOwnClasses;
        private readonly UpdateClassUseCase _updateClassUseCase;
        private readonly DeleteClassUseCase _deleteClassUseCase;

        public SchoolClassController(CreateSchoolClassUseCase createSchoolClassUseCase, AssignTeacherUseCase assignTeacherUseCase,
            GetAllClassUseCase getAllClassUseCase, GetClassesWithoutTeacher getClassesWithoutTeacher,
            GetAllClassesWithTeacher getAllClassesWithTeacher, GetClassByIdUseCase getClassByIdUseCase,
            GetTeacherOwnClasses getOwnClasses, UpdateClassUseCase updateClassUseCase, DeleteClassUseCase deleteClassUseCase
            )
        {
            _createSchoolClassUseCase = createSchoolClassUseCase;
            _assignTeacherUseCase = assignTeacherUseCase;
            _getAllClassUseCase = getAllClassUseCase;
            _getClassesWithoutTeacher = getClassesWithoutTeacher;
            _getAllClassesWithTeacher = getAllClassesWithTeacher;
            _getClassByIdUseCase = getClassByIdUseCase;
            _getOwnClasses = getOwnClasses;
            _updateClassUseCase = updateClassUseCase;
            _deleteClassUseCase = deleteClassUseCase;
        }

        // admin
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
        [Authorize(Roles = "Admin,Student,Teacher")]
        public async Task<ActionResult<List<SchoolClassDTO>>> GetAllClasses([FromQuery] PaginationDTO pagination)
        {
            return Ok(await _getAllClassUseCase.Execute(pagination));
        }

        [HttpGet("without-teacher")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<List<SchoolClassDTO>>> GetAllClassesWithoutATeacher([FromQuery] PaginationDTO pagination)
        {
            return Ok(await _getClassesWithoutTeacher.Execute(pagination));
        }

        [HttpGet("with-teacher")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<List<SchoolClassDTO>>> GetAllClassesWithATeacher([FromQuery] PaginationDTO pagination)
        {
            return Ok(await _getAllClassesWithTeacher.Execute(pagination));
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<List<SchoolClassDTO>>> GetClassById([FromRoute] Guid id)
        {
            return Ok(await _getClassByIdUseCase.Execute(id));
        }

        [HttpPut("{classId}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<ApiResponse<SchoolClassDTO>>> UpdateClassName([FromBody] UpdateClassNameRequest request,
          [FromRoute] Guid classId)
        {
            var schoolClass = new UpdateClassNameDTO(request.Name);
            var result = await _updateClassUseCase.Execute(schoolClass, classId);
            return Ok(new ApiResponse<SchoolClassDTO>
            {
                Message = "School Class Updated Successfully",
                Data = result
            });
        }

        [HttpDelete("{classId}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> DeleteClass([FromRoute] Guid classId)
        {
            await _deleteClassUseCase.Execute(classId);
            return NoContent();
        }


        // teachers
        [HttpGet("own-classes")]
        [Authorize(Roles = "Teacher")]
        public async Task<ActionResult<List<SchoolClassDTO>>> GetAllOwnClasses([FromRoute] PaginationDTO pagination)
        {
            return Ok(await _getOwnClasses.Execute(pagination));
        }
    }
}

// get own class by id student must be populated by enrollement entity that the enrollment status is approved