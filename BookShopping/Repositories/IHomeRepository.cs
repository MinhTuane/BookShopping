namespace BookShopping
{
    public interface IHomeRepository
    {
        Task<IEnumerable<Book>> GetBooks(string Sterm = "", int GenreId = 0);
    }
}