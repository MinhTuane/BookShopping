using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookShopping.Controllers
{

    [Authorize]
    public class CartController : Controller
    {
        private readonly ICartRepository _cartRepo;

        public CartController(ICartRepository cartRepo)
        {
            _cartRepo = cartRepo;
        }
        public async Task<IActionResult> AddItem(int bookId, int qty = 1, int redirect = 0)
        {
            if (ModelState.IsValid)
            {
                var cartCount = await _cartRepo.AddItem(bookId, qty);
                if (redirect == 0)
                    return Ok(cartCount);
            }

            return RedirectToAction("GetUserCart");
        }

        public async Task<IActionResult> GetDiscount(string code)
        {
            var discount = await _cartRepo.GetDiscounts(code);
            if (discount == null)
            {
                ViewData["DcMessage"] = "Wrong Discount Code";
            }
            else
            {
                ViewData["DcMessage"] = "Apply Discount Successful";
                ViewData["Discount"] = discount.Rate;
            }
            var cart = await _cartRepo.GetUserCart();
            return View("GetUserCart", cart);
        }
        public async Task<IActionResult> RemoveItem(int bookId)
        {
            if (ModelState.IsValid)
            {
                var cartCount = await _cartRepo.RemoveItem(bookId);
            }
            return RedirectToAction("GetUserCart");
        }
        public async Task<IActionResult> GetUserCart()
        {
            var cart = await _cartRepo.GetUserCart();
            return View(cart);
        }

        public async Task<IActionResult> GetTotalItemInCart()
        {
            int cartItem = await _cartRepo.GetCartItemCount();
            return Ok(cartItem);
        }

        public IActionResult Checkout(CheckoutModel model, double totalPrice = 0)
        {
            model.TotalAmount = totalPrice;
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> CheckoutDetail(CheckoutModel model)
        {
            if (!ModelState.IsValid)
                return View("Checkout", model);
            bool isCheckedOut = await _cartRepo.DoCheckout(model);
            if (!isCheckedOut)
                return RedirectToAction(nameof(OrderFailure));
            return RedirectToAction(nameof(OrderSuccess));
        }

        public IActionResult OrderSuccess()
        {
            return View();
        }

        public IActionResult OrderFailure()
        {
            return View();
        }

    }
}
