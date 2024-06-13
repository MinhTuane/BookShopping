using BookShopping.Contents;
using BookShopping.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

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
            if (userInDb is null)
            {
                await UserMng.CreateAsync(admin, "Admin@123");
                await UserMng.AddToRoleAsync(admin, Roles.Admin.ToString());
            }

            //        using (var context = new BookDbContext(
            //       service.GetRequiredService<
            //           DbContextOptions<BookDbContext>>()))
            //        {
            //            var genres = new List<Genre>
            //                    {
            //                        new Genre { Id = 1, Name = "Adventure" },
            //    new Genre { Id = 2, Name = "Fantasy" },
            //    new Genre { Id = 3, Name = "Science Fiction" },
            //    new Genre { Id = 4, Name = "Mystery" },
            //    new Genre { Id = 5, Name = "Thriller" },
            //    new Genre { Id = 6, Name = "Romance" },
            //    new Genre { Id = 7, Name = "Horror" },
            //    new Genre { Id = 8, Name = "Historical Fiction" },
            //    new Genre { Id = 9, Name = "Non-Fiction" },
            //    new Genre { Id = 10, Name = "Biography" },
            //    new Genre { Id = 11, Name = "Self-Help" },
            //    new Genre { Id = 12, Name = "Graphic Novel" },
            //    new Genre { Id = 13, Name = "Memoir" },
            //    new Genre { Id = 14, Name = "Classic" },
            //    new Genre { Id = 15, Name = "Poetry" },
            //    new Genre { Id = 16, Name = "Crime" },
            //    new Genre { Id = 17, Name = "Drama" },
            //    new Genre { Id = 18, Name = "Young Adult" },
            //    new Genre { Id = 19, Name = "Children's" },
            //    new Genre { Id = 20, Name = "Dystopian" },
            //    new Genre { Id = 21, Name = "Humor" },
            //    new Genre { Id = 22, Name = "Travel" },
            //    new Genre { Id = 23, Name = "Spiritual" },
            //    new Genre { Id = 24, Name = "Philosophy" },
            //    new Genre { Id = 25, Name = "Educational" }
            //                    };

            //            if (!context.Books.Any())
            //            {
            //                context.Books.AddRange(
            //                        new Book
            //                        {
            //                            Name = "The Hobbit",
            //                            Author = "J.R.R. Tolkien",
            //                            Price = 10.99,
            //                            GenreId = 27, // Fantasy
            //                            Image = "https://example.com/images/the-hobbit.jpg"
            //                        },
            //new Book
            //{
            //    Name = "1984",
            //    Author = "George Orwell",
            //    Price = 9.99,
            //    GenreId = 45, // Dystopian
            //    Image = "https://example.com/images/1984.jpg"
            //},
            //new Book
            //{
            //    Name = "To Kill a Mockingbird",
            //    Author = "Harper Lee",
            //    Price = 7.99,
            //    GenreId = 39, // Classic
            //    Image = "https://example.com/images/to-kill-a-mockingbird.jpg"
            //},
            //new Book
            //{
            //    Name = "The Catcher in the Rye",
            //    Author = "J.D. Salinger",
            //    Price = 8.99,
            //    GenreId = 39, // Classic
            //    Image = "https://example.com/images/the-catcher-in-the-rye.jpg"
            //},
            //new Book
            //{
            //    Name = "The Great Gatsby",
            //    Author = "F. Scott Fitzgerald",
            //    Price = 10.99,
            //    GenreId = 39, // Classic
            //    Image = "https://example.com/images/the-great-gatsby.jpg"
            //},
            //new Book
            //{
            //    Name = "Moby-Dick",
            //    Author = "Herman Melville",
            //    Price = 12.99,
            //    GenreId = 26, // Adventure
            //    Image = "https://example.com/images/moby-dick.jpg"
            //},
            //new Book
            //{
            //    Name = "Pride and Prejudice",
            //    Author = "Jane Austen",
            //    Price = 6.99,
            //    GenreId = 31, // Romance
            //    Image = "https://example.com/images/pride-and-prejudice.jpg"
            //},
            //new Book
            //{
            //    Name = "The Da Vinci Code",
            //    Author = "Dan Brown",
            //    Price = 11.99,
            //    GenreId = 30, // Thriller
            //    Image = "https://example.com/images/the-da-vinci-code.jpg"
            //},
            //new Book
            //{
            //    Name = "Harry Potter and the Sorcerer's Stone",
            //    Author = "J.K. Rowling",
            //    Price = 8.99,
            //    GenreId = 27, // Fantasy
            //    Image = "https://example.com/images/harry-potter-and-the-sorcerers-stone.jpg"
            //},
            //new Book
            //{
            //    Name = "The Hunger Games",
            //    Author = "Suzanne Collins",
            //    Price = 9.99,
            //    GenreId = 45, // Dystopian
            //    Image = "https://example.com/images/the-hunger-games.jpg"
            //}
            //                    );
            //                context.SaveChanges();
            //            }

            //        }
        }
    }
}
