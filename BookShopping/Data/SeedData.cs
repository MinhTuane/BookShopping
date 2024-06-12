using Microsoft.AspNetCore.Identity;

namespace BookShopping.Data
{
    public class SeedData
    {
        public static void SeedDefaultData(IServiceProvider service)
        {
            var UserMng = service.GetService<UserManager<IdentityUser>>();
            var UserMng = service.GetService<UserManager<IdentityUser>>();
        }
    }
}
