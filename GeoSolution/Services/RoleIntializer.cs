using GeoSolution.Models;
using Microsoft.AspNetCore.Identity;

namespace GeoSolution.Services
{
    public static class RoleInitializer
    {
        public static async Task InitializeAsync(UserManager<ApplicationUserModel> userManager, RoleManager<IdentityRole> roleManager)
        {
            // role list
            var roles = new[] { "Admin", "User" };

            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }
            }

            const string adminEmail = "admin@gmail.com";
            const string adminPassword = "admin123";

            if (await userManager.FindByEmailAsync(adminEmail) == null)
            {
                var admin = new ApplicationUserModel
                {
                    Email = adminEmail,
                    UserName = adminEmail
                };
                var result = await userManager.CreateAsync(admin, adminPassword);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(admin, "Admin");
                }
            }
        }
    }
}
