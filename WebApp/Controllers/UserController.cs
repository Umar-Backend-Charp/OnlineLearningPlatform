using Domain.Dto;
using Domain.Dto.Auth;
using Domain.Dto.StudentSummary;
using Domain.Dto.User;
using Domain.Entities;
using Domain.Filter;
using Infrastructure.Response;
using Infrastructure.Services;
using Infrastructure.Services.Auth;
using Infrastructure.Services.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [Authorize(Roles = "Admin")]
    [HttpGet]
    public async Task<PaginationResponse<List<GetUserDto>>> GetUsers([FromQuery] UserFilter filter)
        => await _userService.GetUsers(filter);
    
    [Authorize(Roles = "Admin")]
    [HttpPut]
    public async Task<Response<string>> UpdateUser([FromBody] UpdateUserDto dto)
        => await _userService.UpdateUser(dto);
    
    [Authorize(Roles = "Admin")]
    [HttpDelete("{id}")]
    public async Task<Response<string>> DeleteUser(string id)
        => await _userService.DeleteUser(id);
    
    [Authorize(Roles = "Admin")]
    [HttpGet("{id}")]
    public async Task<Response<GetUserDto>> GetUser(string id)
        => await _userService.GetUser(id);

    [Authorize(Roles = "User, Admin")]
    [HttpGet("{id}/courses/summary")]
    public async Task<Response<StudentSummaryDto>> GetUserSummary(string id)
        => await _userService.GetStudentSummary(id);
}