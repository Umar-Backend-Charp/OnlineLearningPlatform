using Domain.Dto;
using Domain.Dto.User;
using Domain.Filter;
using Infrastructure.Response;

namespace Infrastructure.Services.User;

public interface IUserService
{
    Task<Response<string>> UpdateUser(UpdateUserDto dto);
    Task<Response<string>> DeleteUser(string id);
    Task<PaginationResponse<List<GetUserDto>>> GetUsers(UserFilter filter);
    Task<Response<GetUserDto>> GetUser(string id);
}