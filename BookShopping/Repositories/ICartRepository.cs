namespace BookShopping
{
    public interface ICartRepository
    {
        Task<int> AddItem(int bookId,int qty);
        Task<int> RemoveItem(int bookId);
        Task<ShoppingCart> GetCarts();
        Task<int> GetCartCount(string userId = "");
    }
}