using Application.DTOs;
using Application.UseCases;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using WebAPI.DTOs;
using WebAPI.Requests;

namespace WebAPI.Controllers
{

    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly CreateAdminUserUseCase _createAdminUserUseCase;
        private readonly CreateTeacherUseCase _createTeacherUseCase;
        private readonly CreateStudentUseCase _createStudentUseCase;
        private readonly GetUserByIdUseCase _getUserByIdUseCase;
        private readonly LoginUseCase _loginUseCase;

        public AuthController(CreateAdminUserUseCase createAdminUserUseCase, CreateTeacherUseCase createTeacherUseCase,
          CreateStudentUseCase createStudentUseCase, GetUserByIdUseCase getUserByIdUseCase, LoginUseCase loginUseCase
          )
        {
            _createAdminUserUseCase = createAdminUserUseCase;
            _createTeacherUseCase = createTeacherUseCase;
            _createStudentUseCase = createStudentUseCase;
            _getUserByIdUseCase = getUserByIdUseCase;
            _loginUseCase = loginUseCase;

        }

        [HttpPost("admin")]
        public async Task<ActionResult<UserDTO>> CreateAdmin([FromBody] CreateUserRequests requests)
        {
            var admin = UserRequestMapper.ToDTO(requests);
            var result = await _createAdminUserUseCase.Execute(admin);
            return Ok(result);
        }

        [HttpPost("teacher")]
        public async Task<ActionResult<UserDTO>> CreateTeacher([FromBody] CreateUserRequests requests)
        {
            var teacher = UserRequestMapper.ToDTO(requests);
            var result = await _createTeacherUseCase.Execute(teacher);
            return Ok(result);
        }


        [HttpPost("student")]
        public async Task<ActionResult<UserDTO>> CreateStudent([FromBody] CreateUserRequests requests)
        {
            var student = UserRequestMapper.ToDTO(requests);
            var result = await _createStudentUseCase.Execute(student);
            return result;
        }

        [HttpGet("me")]
        public async Task<ActionResult<UserDTO>> GetMe(Guid id)
        {
            var user = await _getUserByIdUseCase.Execute(id);
            return Ok(user);
        }


        [HttpPost("login")]
        public async Task<ActionResult<LoginResponse>> Login(LoginUserRequest request)
        {
            var user = new LoginDTO(request.Username, request.Password);
            var result = await _loginUseCase.Execute(user);
            return Ok(result);
        }

    }
}
