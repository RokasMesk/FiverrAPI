using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using fiverr_api.Models;
using fiverr_api.Models.DTOs;
using fiverr_api.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace fiverr_api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController(IAuthService authService) : ControllerBase
{
   
    [HttpPost("register")]
    public async Task<ActionResult<User>> Register([FromBody] UserDTO request)
    {
        var user = await authService.RegisterAsync(request);
        if (user == null)
        {
            return BadRequest(new { message = "Username already exists" });
        }

        return Ok(user);
    }

    [HttpPost("login")]
    public async Task<ActionResult<TokenResponseDTO>> Login([FromBody] UserDTO request)
    {
        var response = await authService.LoginAsync(request);
        if (response is null)
        {
            return BadRequest("Invalid username or password");
        }

        return Ok(response);
    }

    [Authorize(Roles = "Admin")]
    [HttpGet("admin-only")]
    public IActionResult Get()
    {
        return Ok("You Are an admin");
    }

    [HttpPost("refresh-token")]
    public async Task<ActionResult<TokenResponseDTO>> RefreshToken(RefreshTokenRequestDTO request)
    {
        var result = await authService.RefreshTokensAsync(request);
        if (result == null || result.AccessToken == null || result.RefreshToken == null)
        {
            return Unauthorized("Invalid refresh token");
        }
        return Ok(result);
    }

    
}