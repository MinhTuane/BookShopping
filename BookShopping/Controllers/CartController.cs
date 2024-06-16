using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookShopping.Controllers
{
    [Authorize]
    public class CartController : Controller
    {
        private readonly ICartRepository cartRepo;

        public CartController(ICartRepository cartRepo)
        {
            this.cartRepo = cartRepo;
        }
        [HttpPost]
        public async Task<IActionResult> AddItem(int bookId,int qty=1,int Redirect =0)
        {
            var cartCount = await cartRepo.AddItem(bookId, qty);
            if(Redirect ==0)
                return Ok(cartCount);
            return RedirectToAction("GetUserCart");
            
            
        }
        [HttpPost]
        public async Task<IActionResult> RemoveItem(int bookId)
        {
            var cartCount = await cartRepo.RemoveItem(bookId);
            return RedirectToAction("GetUserCart");
        }
        public async Task<IActionResult> GetUserCart()
        {
            var cart = await cartRepo.GetCarts();
            return View(cart);
        }
        public async Task<IActionResult> GetTotalItemInCart()
        {
            int cartItem = await cartRepo.GetCartCount();
            return Ok(cartItem);
        }

    }
}
