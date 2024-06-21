using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookShopping.Controllers
{
    [Authorize]
    public class UserOrderController : Controller
    {
        private readonly IUserOrderRepository _userOrderRepo;

        public UserOrderController(IUserOrderRepository userOrderRepo)
        {
            _userOrderRepo = userOrderRepo;
        }
        public async Task<IActionResult> UserOrders()
        {
            var orders = await _userOrderRepo.UserOrder();
            var rank = new Rank(orders.Select(o => o.TotalPrice).Sum());
            UserOrderModel model = new()
            {
                Orders = orders,
                Rank = rank.rankUser
            };
            return View(model);
        }
    }
}
