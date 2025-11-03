using Domain.Entities;
using Domain.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Seed;

public class Seeder(UserManager<User> userManager, RoleManager<IdentityRole> roleManager, DataContext context)
{
    public async Task<bool> SeedRoles() 
    {
        var newRoles = new List<IdentityRole>()
        {
            new IdentityRole(nameof(Role.Student)),
            new IdentityRole(nameof(Role.Teacher)),
            new IdentityRole("Admin")
        };

        var roles = await roleManager.Roles.ToListAsync();

        foreach (var role in newRoles)
        {
            if (roles.Exists(e => e.Name == role.Name))
            {
                continue;
            }
            await roleManager.CreateAsync(role);
        }
        
        return true;
    }

    public async Task<bool> SeedAdmin()
    {
        var res = await context.Users.FirstOrDefaultAsync(x => x.FirstName == "Admin" && x.LastName == "Adminov");
        if (res == null)
        {
            var admin = new User()
            {
                FirstName = "Admin",
                LastName = "Adminov",
                Email = "rahimzodaumar07@gmail.com",
                PhoneNumber = "935668440",
                Address = "Yakachinor",
                Age = 16,
                UserName = "Admin",
            };

            var createResult = await userManager.CreateAsync(admin, "1234abcd");
            await userManager.AddToRoleAsync(admin, "Admin");
            if (!createResult.Succeeded)
            {
                foreach (var error in createResult.Errors)
                    Console.WriteLine(error.Description);
                return false;
            }

            
            if (createResult.Succeeded) return true;
            return false;
        }
        return false;
    }
}