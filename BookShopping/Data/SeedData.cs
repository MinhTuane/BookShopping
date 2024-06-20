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
                //var genres = new List<Genre>
                //                {
                //                    new Genre { GenreName = "Adventure" },
                //new Genre {  GenreName = "Fantasy" },
                //new Genre {  GenreName = "Science Fiction" },
                //new Genre {  GenreName = "Mystery" },
                //new Genre {  GenreName = "Thriller" },
                //new Genre {  GenreName = "Romance" },
                //new Genre {  GenreName = "Horror" },
                //new Genre {  GenreName = "Historical Fiction" },
                //new Genre {  GenreName = "Non-Fiction" },
                //new Genre { GenreName = "Biography" },
                //new Genre {  GenreName = "Self-Help" },
                //new Genre {  GenreName = "Graphic Novel" },
                //new Genre {  GenreName = "Memoir" },
                //new Genre {  GenreName = "Classic" },
                //new Genre {  GenreName = "Poetry" },
                //new Genre {  GenreName = "Crime" },
                //new Genre {  GenreName = "Drama" },
                //new Genre {  GenreName = "Young Adult" },
                //new Genre { GenreName = "Children's" },
                //new Genre { GenreName = "Dystopian" },
                //new Genre { GenreName = "Humor" },
                //new Genre { GenreName = "Travel" },
                //new Genre { GenreName = "Spiritual" },
                //new Genre { GenreName = "Philosophy" },
                //new Genre { GenreName = "Educational" }
                //                };
                //if (!context.Genres.Any())
                //{
                //    context.Genres.AddRange(genres);
                //    context.SaveChanges();
                //}

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

