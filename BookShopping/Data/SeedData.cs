using BookShopping.Contents;
using Microsoft.AspNetCore.Identity;

namespace BookShopping.Data
{
    public class SeedData
    {
        public static async Task SeedDefaultData(IServiceProvider service)
        {
            var UserMng = service.GetService<UserManager<IdentityUser>>();
            var RoleMng = service.GetService<RoleManager<IdentityRole>>();

            await RoleMng.CreateAsync(new IdentityRole(Roles.Admin.ToString()));
            await RoleMng.CreateAsync(new IdentityRole(Roles.User.ToString()));

            var admin = new IdentityUser
            {
                UserName = "admin@gmail.com",
                Email = "admin@gmail.com",
                EmailConfirmed = true,
            };

            var userInDb = await UserMng.FindByEmailAsync(admin.Email);
            if(userInDb is null)
            {
                await UserMng.CreateAsync(admin,"Admin@123");
                await UserMng.AddToRoleAsync(admin,Roles.Admin.ToString());
            }
        }
    }
}
