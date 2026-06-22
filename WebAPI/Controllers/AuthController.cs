using Application.DTOs;
using Application.UseCases.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using WebAPI.DTOs;

namespace WebAPI.Controllers
{

    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly CreateAdminUserUseCase _createAdminUserUseCase;
        private readonly CreateTeacherUseCase _createTeacherUseCase;
        private readonly CreateStudentUseCase _createStudentUseCase;
        private readonly LoginUseCase _loginUseCase;

        public AuthController(CreateAdminUserUseCase createAdminUserUseCase, CreateTeacherUseCase createTeacherUseCase,
          CreateStudentUseCase createStudentUseCase, LoginUseCase loginUseCase
          )
        {
            _createAdminUserUseCase = createAdminUserUseCase;
            _createTeacherUseCase = createTeacherUseCase;
            _createStudentUseCase = createStudentUseCase;
            _loginUseCase = loginUseCase;

        }

        [HttpPost("admin")]
        [Authorize(Roles ="Admin")]
        public async Task<ActionResult<ApiResponse<UserDTO>>> CreateAdmin([FromBody] CreateUserRequests requests)
        {
            var admin = UserRequestMapper.ToDTO(requests);
            var result = await _createAdminUserUseCase.Execute(admin);
            return Ok(new ApiResponse<UserDTO>
            {
                Message = "Created Successfully",
                Data = result
            });
        }

        [HttpPost("teacher")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<ApiResponse<UserDTO>>> CreateTeacher([FromBody] CreateUserRequests requests)
        {
            var teacher = UserRequestMapper.ToDTO(requests);
            var result = await _createTeacherUseCase.Execute(teacher);
            return Ok(new ApiResponse<UserDTO>
            {
                Message = "Created Successfully",
                Data = result
            });
        }


        [HttpPost("student")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<ApiResponse<UserDTO>>> CreateStudent([FromBody] CreateUserRequests requests)
        {
            var student = UserRequestMapper.ToDTO(requests);
            var result = await _createStudentUseCase.Execute(student);
            return Ok(new ApiResponse<UserDTO>
            {
                Message = "Created Successfully",
                Data = result
            });
        }


        [HttpPost("login")]
        public async Task<ActionResult<ApiResponse<string>>> Login(LoginUserRequest request)
        {
            var user = new LoginDTO(request.Username, request.Password);
            var result = await _loginUseCase.Execute(user);
            return Ok(new ApiResponse<string>
            {
                Message = "Login Successfully",
                Data = result
            });
        }

    }
}
