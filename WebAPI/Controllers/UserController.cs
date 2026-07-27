using Application.DTOs;
using Application.Features.Auth.Queries;
using Application.Features.Users.Commands;
using Application.Features.Users.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using WebAPI.Constants;
using WebAPI.DTOs;


namespace WebAPI.Controllers
{

    [ApiController]
    [Route("api/users")]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly CreateAdminUserHandler _createAdminUserUseCase;
        private readonly CreateTeacherHandler _createTeacherUseCase;
        private readonly CreateStudentHandler _createStudentUseCase;
        private readonly GetAllActiveUsersHandler _getAllUsersHandler;
        private readonly GetUserByIdHandler _getUserByIdHandler;
        private readonly GetLoginUserHandler _getLoginUserHandler;
        private readonly UpdateUserHandler _updateUserHandler;
        private readonly DeleteUserHandler _deleteUserHandler;
        private readonly GetAllDeactiveUsersHandler _getAllDeactiveUsersHandler;

        public UserController(CreateAdminUserHandler createAdminUserUseCase, CreateTeacherHandler createTeacherUseCase,
             CreateStudentHandler createStudentUseCase,
            GetAllActiveUsersHandler getAllUsersHandler, GetUserByIdHandler getUserByIdHandler,
            GetLoginUserHandler getLoginUserHandler, UpdateUserHandler updateUserHandler, DeleteUserHandler deleteUserHandler,
            GetAllDeactiveUsersHandler getAllDeactiveUsersHandler
            )
        {
            _createAdminUserUseCase = createAdminUserUseCase;
            _createTeacherUseCase = createTeacherUseCase;
            _createStudentUseCase = createStudentUseCase;
            _getAllUsersHandler = getAllUsersHandler;
            _getUserByIdHandler = getUserByIdHandler;
            _getLoginUserHandler = getLoginUserHandler;
            _updateUserHandler = updateUserHandler;
            _deleteUserHandler = deleteUserHandler;
            _getAllDeactiveUsersHandler = getAllDeactiveUsersHandler;
        }

        // creation of admin
        [HttpPost("admin")]
        [Authorize(Roles = Roles.Admin)]
        public async Task<ActionResult<ApiResponse<Guid>>> CreateAdmin([FromBody] CreateUserRequest request,
        CancellationToken cancellationToken)
        {
            var admin = UserRequestMapper.ToDTO(request);
            var result = await _createAdminUserUseCase.Handle(admin, cancellationToken);
            return CreatedAtAction(nameof(GetUserById),
                  new { id = result },
                  new ApiResponse<Guid>
                  {
                      Message = "Created successfully",
                      Data = result
                  }
                );
        }

        // creation of teacher
        [HttpPost("teacher")]
        [Authorize(Roles = Roles.Admin)]
        public async Task<ActionResult<ApiResponse<Guid>>> CreateTeacher([FromBody] CreateUserRequest request,
            CancellationToken cancellationToken)
        {
            var teacher = UserRequestMapper.ToDTO(request);
            var result = await _createTeacherUseCase.Handle(teacher, cancellationToken);
            return CreatedAtAction(
                nameof(GetUserById),
                new { id = result },
                new ApiResponse<Guid>
                {
                    Message = "Created successfully",
                    Data = result
                }
               );
        }

        // creation of student
        [HttpPost("student")]
        [Authorize(Roles = Roles.Admin)]
        public async Task<ActionResult<ApiResponse<Guid>>> CreateStudent([FromBody] CreateUserRequest request,
         CancellationToken cancellationToken)
        {
            var student = UserRequestMapper.ToDTO(request);
            var result = await _createStudentUseCase.Handle(student, cancellationToken);
            return CreatedAtAction(
                nameof(GetUserById),
                new { id = result },
                new ApiResponse<Guid>
                {
                    Message = "Created successfully",
                    Data = result
                }
              );
        }


        [EnableRateLimiting(RateLimitPolicies.GetResources)]
        [HttpGet]
        [Authorize(Roles = Roles.Admin)]
        public async Task<ActionResult<ApiResponse<IEnumerable<UserDTO>>>> GetAllActiveUsers(
            [FromQuery] PaginationRequest request,
            CancellationToken cancellationToken)
        {
            var query = new GetAllActiveUserQuery(request.Page, request.PageSize);
            var result = await _getAllUsersHandler.Handle(query, cancellationToken);
            return new ApiResponse<IEnumerable<UserDTO>>
            {
                Message = "Users retrieved successfully",
                Data = result
            };
        }

        [EnableRateLimiting(RateLimitPolicies.GetResources)]
        [HttpGet("deleted")]
        [Authorize(Roles = Roles.Admin)]
        public async Task<ActionResult<ApiResponse<IEnumerable<UserDTO>>>> GetAllUnActiveUsers(
            [FromQuery] PaginationRequest request,
            CancellationToken cancellationToken)
        {
            var query = new GetAllDeactiveUserQuery(request.Page, request.PageSize);
            var result = await _getAllDeactiveUsersHandler.Handle(query, cancellationToken);
            return new ApiResponse<IEnumerable<UserDTO>>
            {
                Message = "Users retrieved successfully",
                Data = result
            };
        }

        [EnableRateLimiting(RateLimitPolicies.GetResources)]
        [HttpGet("{id}")]
        [Authorize(Roles = $"{Roles.Admin},{Roles.Teacher}")]
        public async Task<ActionResult<ApiResponse<UserDTO>>> GetUserById([FromRoute] Guid id,
            CancellationToken cancellationToken)
        {
            var query = new GetByIdQuery(id);
            var result = await _getUserByIdHandler.Handle(query, cancellationToken);
            return new ApiResponse<UserDTO>
            {
                Message = "User retrieved successfully",
                Data = result
            };
        }

        [HttpGet("me")]
        public async Task<ActionResult<ApiResponse<UserDTO>>> GetMe(CancellationToken cancellationToken)
        {
            var result = await _getLoginUserHandler.Handle(new GetLoginUserQuery(), cancellationToken);
            return new ApiResponse<UserDTO>
            {
                Message = "User retrieved successfully",
                Data = result
            };
        }

        [HttpPatch("{id}")]
        [Authorize(Roles = Roles.Admin)]
        public async Task<ActionResult> UpdateUser([FromBody] UpdateUserRequest request,
            [FromRoute] Guid id,
            CancellationToken cancellationToken)
        {
            var command = new UpdateUserCommand(id, request.Username, request.Password);
            await _updateUserHandler.Handle(command, cancellationToken);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = Roles.Admin)]
        public async Task<ActionResult> DeleteAccount([FromRoute] Guid id, CancellationToken cancellationToken)
        {
            var command = new DeleteUserCommand(id);
            await _deleteUserHandler.Handle(command, cancellationToken);
            return NoContent();
        }
    }
}