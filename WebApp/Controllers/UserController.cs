using Domain.Dto;
using Domain.Dto.Auth;
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
public class UserController
    (IUserService userService) : ControllerBase
{
    [Authorize(Roles = "Admin")]
    [HttpGet]
    public Task<PaginationResponse<List<GetUserDto>>> GetUsers([FromQuery] UserFilter filter)
        => userService.GetUsers(filter);
    
    [Authorize(Roles = "Admin")]
    [HttpPut]
    public Task<Response<string>> UpdateUser(UpdateUserDto dto)
        => userService.UpdateUser(dto);
    
    [Authorize(Roles = "Admin")]
    [HttpDelete]
    public Task<Response<string>> DeleteUser(string id)
        => userService.DeleteUser(id);
    
    [Authorize]
    [HttpGet("{id}")]
    public Task<Response<GetUserDto>> GetUser(string id)
        => userService.GetUser(id);
}

