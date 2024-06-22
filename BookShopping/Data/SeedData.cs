using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BookShopping.Data
{
    public class SeedData
    {
        public static async Task SeedDefaultData(IServiceProvider service)
        {
            var UserMng = service.GetService<UserManager<ApplicationUser>>();
            var RoleMng = service.GetService<RoleManager<IdentityRole>>();

            await RoleMng.CreateAsync(new IdentityRole(Roles.Admin.ToString()));
            await RoleMng.CreateAsync(new IdentityRole(Roles.User.ToString()));

            var admin = new ApplicationUser
            {
                UserName = "admin@gmail.com",
                Email = "admin@gmail.com",
                EmailConfirmed = true,
            };

            var userInDb = await UserMng.FindByEmailAsync(admin.Email);
            if (userInDb is null)
            {
                await UserMng.CreateAsync(admin, "Admin@123");
                await UserMng.AddToRoleAsync(admin, Roles.Admin.ToString());
            }

            using (var context = new BookDbContext(
           service.GetRequiredService<
               DbContextOptions<BookDbContext>>()))
            {
                var genres = new List<Genre>
                                {
                                    new Genre { GenreName = "Adventure" },
                new Genre {  GenreName = "Fantasy" },
                new Genre {  GenreName = "Science Fiction" },
                new Genre {  GenreName = "Mystery" },
                new Genre {  GenreName = "Thriller" },
                new Genre {  GenreName = "Romance" },
                new Genre {  GenreName = "Horror" },
                new Genre {  GenreName = "Historical Fiction" },
                new Genre {  GenreName = "Non-Fiction" },
                new Genre { GenreName = "Biography" },
                new Genre {  GenreName = "Self-Help" },
                new Genre {  GenreName = "Graphic Novel" },
                new Genre {  GenreName = "Memoir" },
                new Genre {  GenreName = "Classic" },
                new Genre {  GenreName = "Poetry" },
                new Genre {  GenreName = "Crime" },
                new Genre {  GenreName = "Drama" },
                new Genre {  GenreName = "Young Adult" },
                new Genre { GenreName = "Children's" },
                new Genre { GenreName = "Dystopian" },
                new Genre { GenreName = "Humor" },
                new Genre { GenreName = "Travel" },
                new Genre { GenreName = "Spiritual" },
                new Genre { GenreName = "Philosophy" },
                new Genre { GenreName = "Educational" }
                                };
                if (!context.Genres.Any())
                {
                    context.Genres.AddRange(genres);
                    context.SaveChanges();
                }
                if (!context.OrderStatuses.Any())
                {
                    context.OrderStatuses.AddRange(
                            new OrderStatus { StatusName = "Pending", StatusId = 1 },
                            new OrderStatus { StatusName = "Shipped", StatusId = 2 },
                            new OrderStatus { StatusName = "Delivered", StatusId = 3 },
                            new OrderStatus { StatusName = "Cancelled", StatusId = 4 },
                            new OrderStatus { StatusName = "Returned", StatusId = 5 },
                            new OrderStatus { StatusName = "Refund", StatusId = 6 }
                        );
                }

            }
        }
    }
}

