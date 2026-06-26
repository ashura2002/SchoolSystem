using Application.DTOs;
using Application.UseCases.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using WebAPI.Constants;
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
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<ApiResponse<UserDTO>>> CreateAdmin([FromBody] CreateUserRequests requests,
            CancellationToken cancellationToken)
        {
            var admin = UserRequestMapper.ToDTO(requests);
            var result = await _createAdminUserUseCase.Execute(admin, cancellationToken);
            return Ok(new ApiResponse<UserDTO>
            {
                Message = "Created Successfully",
                Data = result
            });
        }

        [HttpPost("teacher")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<ApiResponse<UserDTO>>> CreateTeacher([FromBody] CreateUserRequests requests,
            CancellationToken cancellationToken)
        {
            var teacher = UserRequestMapper.ToDTO(requests);
            var result = await _createTeacherUseCase.Execute(teacher, cancellationToken);
            return Ok(new ApiResponse<UserDTO>
            {
                Message = "Created Successfully",
                Data = result
            });
        }


        [HttpPost("student")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<ApiResponse<UserDTO>>> CreateStudent([FromBody] CreateUserRequests requests,
            CancellationToken cancellationToken)
        {
            var student = UserRequestMapper.ToDTO(requests);
            var result = await _createStudentUseCase.Execute(student, cancellationToken);
            return Ok(new ApiResponse<UserDTO>
            {
                Message = "Created Successfully",
                Data = result
            });
        }


        [EnableRateLimiting(RateLimitPolicies.Login)]
        [HttpPost("login")]
        public async Task<ActionResult<ApiResponse<string>>> Login(LoginUserRequest request, CancellationToken cancellationToken)
        {
            var user = new LoginDTO(request.Username, request.Password);
            var result = await _loginUseCase.Execute(user, cancellationToken);
            return Ok(new ApiResponse<string>
            {
                Message = "Login Successfully",
                Data = result
            });
        }

    }
}
