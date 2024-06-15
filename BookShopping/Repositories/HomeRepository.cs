
using Microsoft.EntityFrameworkCore;

namespace BookShopping.Repositories
{
    public class HomeRepository : IHomeRepository
    {
        private readonly BookDbContext _db;

        public HomeRepository(BookDbContext db)
        {
            _db = db;
        }
        public async Task<IEnumerable<Book>> GetBooks(string Sterm="",int GenreId =0)
        {
            Sterm =Sterm.ToLower();
            IEnumerable<Book> books = await (from book in _db.Books
                                             join genre in _db.Genres
                                             on book.GenreId equals genre.Id
                                             where string.IsNullOrEmpty(Sterm) || (book != null && book.Name.ToLower().StartsWith(Sterm))
                                             select new Book
                                             {
                                                 Id=book.Id,
                                                 Name=book.Name,
                                                 Image=book.Image,
                                                 Author=book.Author,
                                                 GenreId=book.GenreId,
                                                 Price=book.Price,
                                                 GenreName=genre.Name
                                             }
                                             ).ToListAsync();
            if(GenreId >0)
            {
                books= books.Where(book => book.GenreId == GenreId).ToList();   
            }
            return books;
        }
        
    }
}
