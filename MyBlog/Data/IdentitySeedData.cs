using Microsoft.AspNetCore.Identity;

namespace MyBlog.Data;

public class IdentitySeedData
{
    public static async Task Initialize(BlogDbContext context,
        UserManager<IdentityUser> userManager,
        RoleManager<IdentityRole> roleManager)
    {
        context.Database.EnsureCreated();

        string asdminRole = "Admin";
        string userRole = "User";
        string password4all = "asdfgh";

        if (await roleManager.FindByNameAsync(asdminRole) == null)
        {
            await roleManager.CreateAsync(new IdentityRole(asdminRole));
        }

        if (await roleManager.FindByNameAsync(userRole) == null)
        {
            await roleManager.CreateAsync(new IdentityRole(userRole));
        }

        if (await userManager.FindByNameAsync("aa@aa.aa") == null)
        {
            var user = new IdentityUser
            {
                UserName = "aa@aa.aa",
                Email = "aa@aa.aa"
               
            };

            var result = await userManager.CreateAsync(user);
            if (result.Succeeded)
            {
                await userManager.AddPasswordAsync(user, password4all);
                await userManager.AddToRoleAsync(user, asdminRole);
            }
        }

        if (await userManager.FindByNameAsync("mm@mm.mm") == null)
        {
            var user = new IdentityUser
            {
                UserName = "mm@mm.mm",
                Email = "mm@mm.mm"
               
            };

            var result = await userManager.CreateAsync(user);
            if (result.Succeeded)
            {
                await userManager.AddPasswordAsync(user, password4all);
                await userManager.AddToRoleAsync(user, userRole);
            }
        }
    }
}