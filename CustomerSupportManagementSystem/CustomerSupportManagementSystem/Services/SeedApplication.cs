using CustomerSupportManagementSystem.Models;
using Microsoft.AspNetCore.Identity;

namespace CustomerSupportManagementSystem.Services
{
    public static class SeedApplication
    {
        public static async void SeedRoles(IApplicationBuilder app)
        {
            var scope = app.ApplicationServices.CreateScope();
            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            // Define roles
            var roles = new[] { "Admin", "SupportAgent", "Customer" };

            foreach (var role in roles)
            {
                var roleExist = roleManager.RoleExistsAsync(role).Result;
                if (!roleExist)
                {
                    var roleResult = roleManager.CreateAsync(new IdentityRole(role)).Result;
                }
            }

            var user = new ApplicationUser { UserName = "admin@cognine.com", Email = "admin@cognine.com" };
            var result = await  userManager.CreateAsync(user, "Admin@123");
            if (result.Succeeded)
            {
               await userManager.AddToRoleAsync(user, "Admin");
            }
        }
    }
}
