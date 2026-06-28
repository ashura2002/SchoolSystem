using Application.DTOs;
using Application.Features.Auth.Commands;
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
        private readonly CreateAdminUserHandler _createAdminUserUseCase;
        private readonly CreateTeacherHandler _createTeacherUseCase;
        private readonly CreateStudentHandler _createStudentUseCase;
        private readonly LoginHandler _loginUseCase;

        public AuthController(CreateAdminUserHandler createAdminUserUseCase, CreateTeacherHandler createTeacherUseCase,
          CreateStudentHandler createStudentUseCase, LoginHandler loginUseCase
          )
        {
            _createAdminUserUseCase = createAdminUserUseCase;
            _createTeacherUseCase = createTeacherUseCase;
            _createStudentUseCase = createStudentUseCase;
            _loginUseCase = loginUseCase;

        }

        [HttpPost("admin")]
        [Authorize(Roles = Roles.Admin)]
        public async Task<ActionResult<ApiResponse<UserDTO>>> CreateAdmin([FromBody] CreateUserRequests requests,
            CancellationToken cancellationToken)
        {
            var admin = UserRequestMapper.ToDTO(requests);
            var result = await _createAdminUserUseCase.Handle(admin, cancellationToken);
            return Ok(new ApiResponse<UserDTO>
            {
                Message = "Created Successfully",
                Data = result
            });
        }

        [HttpPost("teacher")]
        [Authorize(Roles = Roles.Admin)]
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
        [Authorize(Roles = Roles.Admin)]
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
            var user = new LoginCommand(request.Username, request.Password);
            var result = await _loginUseCase.Handle(user, cancellationToken);
            return Ok(new ApiResponse<string>
            {
                Message = "Login Successfully",
                Data = result
            });
        }

    }
}
