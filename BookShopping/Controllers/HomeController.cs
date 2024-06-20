using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace BookShopping.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IHomeRepository _homeRepository;
        private readonly BookDbContext context;

        public HomeController(ILogger<HomeController> logger, IHomeRepository homeRepository, BookDbContext context)
        {
            _logger = logger;
            _homeRepository = homeRepository;
            this.context = context;
        }

        public async Task<IActionResult> Index(int? pageNumber, string currentFilter, string sterm = "", int genreId = 0)
        {
            ViewData["Genres"] = await context.Genres.ToListAsync();
            ViewData["genreId"] = genreId;
            if (!string.IsNullOrEmpty(sterm))
            {
                pageNumber = 1;
            }
            else
            {
                sterm = currentFilter;
            }
            ViewData["CurrentFillter"] = sterm;

            var books = from book in context.Books
                        join genre in context.Genres
                        on book.GenreId equals genre.Id
                        join stock in context.Stocks
                        on book.Id equals stock.BookId
                        into book_stocks
                        from bookWithStock in book_stocks.DefaultIfEmpty()
                        select new Book
                        {
                            Id = book.Id,
                            Image = book.Image,
                            Author = book.Author,
                            Name = book.Name,
                            GenreId = book.GenreId,
                            Price = book.Price,
                            GenreName = genre.GenreName,
                            Quantity = bookWithStock == null ? 0 : bookWithStock.Quantity
                        };

            if (!string.IsNullOrEmpty(sterm))
            {
                books = books.Where(b => b.Name.Contains(sterm));
            }

            if (genreId > 0)
            {
                books = books.Where(b => b.GenreId == genreId);
            }
            int pageSize = 3;
            return View(await PaginatedList<Book>.CreateAsync(books.AsNoTracking(), pageNumber ?? 1, pageSize));
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
