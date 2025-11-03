using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using Domain.Dto.Auth;
using Domain.Entities;
using Infrastructure.Data;
using Infrastructure.Response;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Infrastructure.Services.Auth;

public class AuthService(
    DataContext context, 
    UserManager<Domain.Entities.User> userManager,
    IMapper mapper,
    IConfiguration configuration) : IAuthService
{
    public async Task<Response<string>> RegisterUserAsync(Register model)
    {
        var existingUser = await userManager.FindByEmailAsync(model.Email);
        if (existingUser != null) return new Response<string>(HttpStatusCode.BadRequest,$"User with email {model.Email} already exists");

        var user = mapper.Map<Domain.Entities.User>(model);
        user.UserName = user.FirstName + user.LastName;
        var result = await userManager.CreateAsync(user, model.Password);
        await userManager.AddToRoleAsync(user, "Student");

        if (!result.Succeeded)
        {
            return new Response<string>(HttpStatusCode.BadRequest, result.Errors.First().Description);
        }
        
        return new Response<string>(HttpStatusCode.OK, "User registered successfully");
    }

    public async Task<Response<string>> LoginAsync(Login model)
    {
        var user = await context.Users.FirstOrDefaultAsync(u => u.Email == model.Email);
        if (user is null) 
            return new Response<string>(HttpStatusCode.BadRequest, "Invalid email or password");
        
        if (!await userManager.CheckPasswordAsync(user, model.Password)) 
            return new Response<string>(HttpStatusCode.BadRequest, "Invalid email or password");
        
        var token = await GenerateJwtTokenAsync(user);
        return new Response<string>(token);
    }

    private async Task<string> GenerateJwtTokenAsync(Domain.Entities.User user)
    {
        var key = Encoding.UTF8.GetBytes(configuration["Jwt:Key"]!);
        var security = new SymmetricSecurityKey(key);
        var credentials = new SigningCredentials(security, SecurityAlgorithms.HmacSha256);

        var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(JwtRegisteredClaimNames.Email, user.Email!),
            new Claim("first-name", user.FirstName),
            new Claim("last-name", user.LastName),
            new Claim("age", user.Age.ToString()),
            new Claim("address", user.Address),
        };

        var userRoles = await userManager.GetRolesAsync(user);
        claims.AddRange(userRoles.Select(role => new Claim(ClaimTypes.Role, role)));

        var tokenDescription = new JwtSecurityToken(
            issuer: configuration["Jwt:Issuer"],
            audience: configuration["Jwt:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(int.Parse(configuration["Jwt:ExpiryMinutes"]!)),
            signingCredentials: credentials
        );
        
        var tokenString = new JwtSecurityTokenHandler().WriteToken(tokenDescription);
        return tokenString;
    }
}