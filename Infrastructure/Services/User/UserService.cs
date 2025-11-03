using System.Net;
using AutoMapper;
using Domain.Dto;
using Domain.Dto.User;
using Domain.Filter;
using Infrastructure.Data;
using Infrastructure.Response;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services.User;

public class UserService(DataContext context, IMapper mapper, UserManager<Domain.Entities.User> userManager) : IUserService
{
    public async Task<Response<string>> UpdateUser(UpdateUserDto dto)
    {
        var user = await context.Users.FirstOrDefaultAsync(x => x.Id == dto.Id);
        if (user == null)
            return new Response<string>(HttpStatusCode.NotFound, "User not found for update");
        mapper.Map(dto, user);
        user.UpdateAt = DateTime.UtcNow;
        var effect = await context.SaveChangesAsync();
        return effect > 0
            ? new Response<string>(HttpStatusCode.OK, "User updated successfully")
            : new Response<string>(HttpStatusCode.BadRequest, "User updating failed");
    }


    public async Task<Response<string>> DeleteUser(string id)
    {
        var user = await userManager.FindByIdAsync(id);
        if (user == null) return new Response<string>(HttpStatusCode.NotFound,  "User not found");
        user.IsDeleted = true;
        var effect = await context.SaveChangesAsync();
        return effect > 0
            ?  new Response<string>(HttpStatusCode.OK, "User deleted successfully")
            : new Response<string>(HttpStatusCode.BadRequest, "User deletion failed");
    }

    public async Task<PaginationResponse<List<GetUserDto>>> GetUsers(UserFilter filter)
    {
        var query = context.Users.Where(x => !x.IsDeleted).AsQueryable();
        // if (filter.Role.HasValue)
        // {
        //     var usersInRole = await userManager.GetUsersInRoleAsync(filter.Role.Value.ToString());
        //     var userIds = usersInRole.Select(x => x.Id).ToList();
        //     query = query.Where(x => userIds.Contains(x.Id));
        // }
        
        if (filter.Age.HasValue)
        {
            query = query.Where(x => x.Age == filter.Age.Value);
        }

        if (!string.IsNullOrEmpty(filter.FirstName))
        {
            query = query.Where(x => x.FirstName.ToLower().Contains(filter.FirstName.ToLower()));
        }


        int totalRecord = await query.CountAsync();
        int skip = (filter.PageNumber - 1) * filter.PageSize;
        var res = await query.Skip(skip).Take(filter.PageSize).ToListAsync();
        if (!res.Any())
        {
            return new PaginationResponse<List<GetUserDto>>(HttpStatusCode.NotFound, "Users not found");
        }
        var usersMap = mapper.Map<List<GetUserDto>>(res);
        return new PaginationResponse<List<GetUserDto>>(usersMap, totalRecord, filter.PageNumber, filter.PageSize);
    }

    public async Task<Response<GetUserDto>> GetUser(string id)
    {
        var theUser = await context.Users.FindAsync(id);
        if (theUser == null) return new Response<GetUserDto>(HttpStatusCode.NotFound, "User not found");
        var map = mapper.Map<GetUserDto>(theUser);
        return new Response<GetUserDto>(map);
    }
}