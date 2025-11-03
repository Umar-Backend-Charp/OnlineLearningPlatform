using Domain.Dto.Auth;
using Infrastructure.Response;

namespace Infrastructure.Services.Auth;

public interface IAuthService
{
    Task<Response<string>> RegisterUserAsync(Register dto);
    Task<Response<string>> LoginAsync(Login dto);
}