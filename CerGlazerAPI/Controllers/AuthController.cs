using CerGlazerAPI.Entities;
using CerGlazerAPI.Models;
using CerGlazerAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CerGlazerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController(IAuthService authService) : ControllerBase
    {
        public static User user = new();

        [HttpPost("login")]
        public async Task<ActionResult<TokenResponseDto>> Login([FromBody] UserDTO userLoginRequest)
        {
            var result = await authService.LoginUserAsync(userLoginRequest);

            if (result == null) 
            { 
                return BadRequest(new { Message = "Incorrect user name or password." });
            }  

            return Ok(result);
        }

        [HttpPost("register")]
        public async Task<ActionResult<User>> Register([FromBody] UserDTO userRegistrationRequest)
        {

            var user = await authService.RegisterUserAsync(userRegistrationRequest);

            if (user == null) 
            { 
                return BadRequest("User already exists.");
            }

            return Ok(String.Concat(user.UserName," succesfully registered."));
        }

        [HttpPost("refresh-token")]
        public async Task<ActionResult<TokenResponseDto>> RefreshToken([FromBody] RefreshTokenRequestDto tokenRefreshRequest)
        {
            var result = await authService.RefreshTokensAsync(tokenRefreshRequest);

            if (result is null ||
                result.AccessToken is null ||
                result.RefreshToken is null)
            {
                return Unauthorized(new { Message = "Invalid refresh token." });
            }

            return Ok(result);
        }

            [Authorize]
        [HttpGet]
        public async Task<IActionResult> AuthenticatedEndPoint()
        {         
            return Ok("Success authenticated connection.");
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("Admin")]
        public async Task<IActionResult> AdminOnlyEndPoint()
        {
            return Ok("Admin access granted.");
        }
    }
}
