﻿
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
        public async Task<IEnumerable<Book>> GetBooks(string sTerm = "", int genreId = 0)
        {
            sTerm = sTerm.ToLower();
            IEnumerable<Book> books = await (from book in _db.Books
                                             join genre in _db.Genres
                                             on book.GenreId equals genre.Id
                                             join stock in _db.Stocks
                                             on book.Id equals stock.BookId
                                             into book_stocks
                                             from bookWithStock in book_stocks.DefaultIfEmpty()
                                             where string.IsNullOrWhiteSpace(sTerm) || (book != null && book.Name.ToLower().StartsWith(sTerm))
                                             select new Book
                                             {
                                                 Id = book.Id,
                                                 Image = book.Image,
                                                 Author = book.Author,
                                                 Name = book.Name,
                                                 GenreId = book.GenreId,
                                                 Price = book.Price,
                                                 GenreName= genre.GenreName,
                                                 Quantity = bookWithStock == null ? 0 : bookWithStock.Quantity
                                             }
                         ).ToListAsync();
            if (genreId > 0)
            {

                books = books.Where(a => a.GenreId == genreId).ToList();
            }
            return books;

        }
    }
}