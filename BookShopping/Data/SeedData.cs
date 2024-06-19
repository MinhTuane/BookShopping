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

            using (var context = new BookDbContext(
           service.GetRequiredService<
               DbContextOptions<BookDbContext>>()))
            {
                var genres = new List<Genre>
                                {
                                    new Genre { Name = "Adventure" },
                new Genre {  Name = "Fantasy" },
                new Genre {  Name = "Science Fiction" },
                new Genre {  Name = "Mystery" },
                new Genre {  Name = "Thriller" },
                new Genre {  Name = "Romance" },
                new Genre {  Name = "Horror" },
                new Genre {  Name = "Historical Fiction" },
                new Genre {  Name = "Non-Fiction" },
                new Genre { Name = "Biography" },
                new Genre {  Name = "Self-Help" },
                new Genre {  Name = "Graphic Novel" },
                new Genre {  Name = "Memoir" },
                new Genre {  Name = "Classic" },
                new Genre {  Name = "Poetry" },
                new Genre {  Name = "Crime" },
                new Genre {  Name = "Drama" },
                new Genre {  Name = "Young Adult" },
                new Genre { Name = "Children's" },
                new Genre { Name = "Dystopian" },
                new Genre { Name = "Humor" },
                new Genre { Name = "Travel" },
                new Genre { Name = "Spiritual" },
                new Genre { Name = "Philosophy" },
                new Genre { Name = "Educational" }
                                };
                if (!context.Genres.Any())
                {
                    context.Genres.AddRange(genres);
                    context.SaveChanges();
                }

                if (!context.Books.Any())
                {
                    context.Books.AddRange(
                            new Book
                            {
                                Name = "The Hobbit",
                                Author = "J.R.R. Tolkien",
                                Price = 10.99,
                                GenreId = 2, // Fantasy
                                Image = "https://example.com/images/the-hobbit.jpg"
                            },
    new Book
    {
        Name = "1984",
        Author = "George Orwell",
        Price = 9.99,
        GenreId = 20, // Dystopian
        Image = "https://example.com/images/1984.jpg"
    },
    new Book
    {
        Name = "To Kill a Mockingbird",
        Author = "Harper Lee",
        Price = 7.99,
        GenreId = 14, // Classic
        Image = "https://example.com/images/to-kill-a-mockingbird.jpg"
    },
    new Book
    {
        Name = "The Catcher in the Rye",
        Author = "J.D. Salinger",
        Price = 8.99,
        GenreId = 14, // Classic
        Image = "https://example.com/images/the-catcher-in-the-rye.jpg"
    },
    new Book
    {
        Name = "The Great Gatsby",
        Author = "F. Scott Fitzgerald",
        Price = 10.99,
        GenreId = 14, // Classic
        Image = "https://example.com/images/the-great-gatsby.jpg"
    },
    new Book
    {
        Name = "Moby-Dick",
        Author = "Herman Melville",
        Price = 12.99,
        GenreId = 5, // Adventure
        Image = "https://example.com/images/moby-dick.jpg"
    },
    new Book
    {
        Name = "Pride and Prejudice",
        Author = "Jane Austen",
        Price = 6.99,
        GenreId = 6, // Romance
        Image = "https://example.com/images/pride-and-prejudice.jpg"
    },
    new Book
    {
        Name = "The Da Vinci Code",
        Author = "Dan Brown",
        Price = 11.99,
        GenreId = 5, // Thriller
        Image = "https://example.com/images/the-da-vinci-code.jpg"
    },
    new Book
    {
        Name = "Harry Potter and the Sorcerer's Stone",
        Author = "J.K. Rowling",
        Price = 8.99,
        GenreId = 2, // Fantasy
        Image = "https://example.com/images/harry-potter-and-the-sorcerers-stone.jpg"
    },
    new Book
    {
        Name = "The Hunger Games",
        Author = "Suzanne Collins",
        Price = 9.99,
        GenreId = 20, // Dystopian
        Image = "https://example.com/images/the-hunger-games.jpg"
    }
                        );
                    context.SaveChanges();
                }

            }
        }
        }
    }

