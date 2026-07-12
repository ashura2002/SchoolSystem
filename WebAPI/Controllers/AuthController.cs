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
        private readonly LoginHandler _loginUseCase;

        public AuthController(
          LoginHandler loginUseCase
          )
        {
            _loginUseCase = loginUseCase;
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
