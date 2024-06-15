using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BookShopping.Repositories
{
    public class CartRepository : ICartRepository
    {
        private readonly BookDbContext db;
        private readonly UserManager<IdentityUser> userManager;
        private readonly IHttpContextAccessor hca;

        public CartRepository(BookDbContext db,UserManager<IdentityUser> userManager,IHttpContextAccessor hca)
        {
            this.db = db;
            this.userManager = userManager;
            this.hca = hca;
        }

        public async Task<bool> AddItem(int bookId,int qty)
        {
            using var transaction = db.Database.BeginTransaction();
            try
            {
                string userId = GetUserId();
                if (string.IsNullOrEmpty(userId)) { return false; }
                var cart = await GetCart(userId);
                if (cart is null)
                {
                    cart = new ShoppingCart()
                    {
                        UserId = userId
                    };
                    db.ShoppingCarts.Add(cart);
                }
                db.SaveChanges();

                var cartItem = db.CartDetails.FirstOrDefault(a => a.ShoppingCartId == cart.Id && a.BookId == bookId);
                if (cartItem is not null)
                {
                    cartItem.Quantity += qty;
                }
                else
                {
                    cartItem = new CartDetail
                    {
                        BookId = bookId,
                        ShoppingCartId = cart.Id,
                        Quantity = qty
                    };
                    db.CartDetails.Add(cartItem);
                }
                db.SaveChanges();
                transaction.Commit();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<bool> RemoveItem(int bookId )
        {
            try
            {
                string userId = GetUserId();
                if(string.IsNullOrEmpty(userId)) { return false; }
                var cart = await GetCart(userId);   
                if(cart is null)
                {
                    return false;
                }
                db.SaveChanges();

                var cartItem = db.CartDetails.FirstOrDefault(a => a.ShoppingCartId == cart.Id && a.BookId == bookId);
                if(cartItem is null)
                {
                    return false;
                }
                else if(cartItem.Quantity ==1)
                {
                    db.CartDetails.Remove(cartItem);
                } 
                else
                {
                    cartItem.Quantity--;
                }
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }          
        }

        public async Task<IEnumerable<ShoppingCart>> GetCarts()
        {
            var userId = GetUserId();
            if(userId ==null)
            {
                throw new Exception("Invalid user id");
            }
            var shoppingCarts = await db.ShoppingCarts
                                    .Include(a => a.CartDetails)
                                    .ThenInclude(a => a.Book)
                                    .ThenInclude(a => a.Genre)
                                    .Where(a => a.UserId == userId)
                                    .ToListAsync();
            return shoppingCarts;
        }
        private async Task<ShoppingCart> GetCart(string userId)
        {
            var cart = await db.ShoppingCarts.FirstOrDefaultAsync(x => x.UserId == userId);
            return cart;
        }

        private string GetUserId()
        {
            var user = hca.HttpContext.User;
            string userId = userManager.GetUserId(user);
            return userId;
        }
    }
}

