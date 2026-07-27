using Application.Features.Auth.Commands;
using Application.Interfaces.CustomeMediatR;
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
        //private readonly LoginHandler _loginUseCase
        private readonly ICustomMediatR _customMediatR;

        public AuthController(
          ICustomMediatR customMediatR
          )
        {
            _customMediatR = customMediatR;
        }


        [EnableRateLimiting(RateLimitPolicies.Login)]
        [HttpPost("login")]
        public async Task<ActionResult<ApiResponse<string>>> Login(LoginUserRequest request, CancellationToken cancellationToken)
        {
            var command = new LoginCommand(request.Username, request.Password);
            var result = await _customMediatR.SendAsync(command, cancellationToken);
            return new ApiResponse<string>
            {
                Message = "Login successfully",
                Data = result
            };
        }

    }
}
