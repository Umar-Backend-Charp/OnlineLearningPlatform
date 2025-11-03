using Domain.Dto.Auth;
using Infrastructure.Response;
using Infrastructure.Services.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController(
    IAuthService authService)
{
    [AllowAnonymous]
    [HttpPost("register")]
    public async Task<Response<string>> Register([FromBody] Register user)
        => await authService.RegisterUserAsync(user);

    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<Response<string>> Login([FromBody] Login model)
        => await authService.LoginAsync(model);
}