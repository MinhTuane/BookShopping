using BookShopping.Models.DTOs;
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

        public async Task<int> AddItem(int bookId,int qty=1)
        {
            using var transaction = db.Database.BeginTransaction();
            string userId = GetUserId();
            try
            {
                if (string.IsNullOrEmpty(userId)) 
                {
                    throw new Exception("user not found");
                 }
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
            }
            catch (Exception ex)
            {
            }
            var totalItem =await GetCartCount(userId);
            return totalItem;
        }

        public async Task<int> RemoveItem(int bookId )
        {
            string userId = GetUserId();
            try
            {
                
                if(string.IsNullOrEmpty(userId)) { throw new Exception("user not found"); }
                var cart = await GetCart(userId);   
                if(cart is null)
                {
                    throw new Exception("cart not found"); 
                }
                db.SaveChanges();

                var cartItem = db.CartDetails.FirstOrDefault(a => a.ShoppingCartId == cart.Id && a.BookId == bookId);
                if(cartItem is null)
                {
                    throw new Exception("No item in cart");
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
            }
            catch (Exception ex)
            {
            }
            var totalItem = await GetCartCount(userId);
            return totalItem;
        }

        public async Task<ShoppingCart> GetCarts()
        {
            var userId = GetUserId();
            if(userId ==null)
            {
                throw new Exception("Invalid user id");
            }
            var shoppingCart = await db.ShoppingCarts
                                    .Include(a => a.CartDetails)
                                        .ThenInclude(c => c.Book)
                     
                                    .Where(a => a.UserId == userId)
                                    .FirstOrDefaultAsync();
            return shoppingCart;
        }
        public async Task<ShoppingCart> GetCart(string userId)
        {
            var cart = await db.ShoppingCarts.FirstOrDefaultAsync(x => x.UserId == userId);
            return cart;
        }

        public async Task<int> GetCartCount(string userId = "")
        {
            if(!string.IsNullOrEmpty(userId))
            {
                userId = GetUserId();
            }
            var data = await (from cart in db.ShoppingCarts
                              join cartDetail in db.CartDetails
                              on cart.Id equals cartDetail.ShoppingCartId
                              select new { cartDetail.Id }
                        ).ToListAsync();
            return data.Count;
        }

        
            private string GetUserId()
        {
            var user = hca.HttpContext.User;
            string userId = userManager.GetUserId(user);
            return userId;
        }

        public async Task<bool> DoCheckout(CheckoutModel model)
        {
            using var transaction = db.Database.BeginTransaction();
            string userId = GetUserId();
            try
            {
                if (string.IsNullOrEmpty(userId))
                {
                    throw new Exception("User not found");
                }

                var cart = await GetCart(userId);
                if (cart == null || !cart.CartDetails.Any())
                {
                    throw new Exception("Cart is empty");
                }
                var pendingRecord = db.OrderStatuses.FirstOrDefault(s => s.StatusName == "Pending");
                if (pendingRecord is null) 
                    throw new Exception("Order status does not have Pending status");

                var order = new Order
                {
                    UserId = userId,
                    OrderStatusId = pendingRecord.Id, 
                    //Name = model.Name,
                    //Email = model.Email,
                    //MobileNumber = model.MobileNumber,
                    //PaymentMethod = model.PaymentMethod,
                    //Address = model.Address,
                    //IsPaid =false,
                    OrderDetails = cart.CartDetails.Select(cd => new OrderDetail
                    {
                        BookId = cd.BookId,
                        Quantity = cd.Quantity,
                        UnitPrice = cd.Book.Price 
                    }).ToList()
                };

                db.Orders.Add(order);
                db.CartDetails.RemoveRange(cart.CartDetails);
                db.ShoppingCarts.Remove(cart);
                await db.SaveChangesAsync();
                await transaction.CommitAsync();

                return true;
            }
            catch (Exception ex)
            {
 
                await transaction.RollbackAsync();
                return false;
            }
        }

    }
}

