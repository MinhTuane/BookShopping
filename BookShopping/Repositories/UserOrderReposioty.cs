using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BookShopping.Repositories
{
    public class UserOrderReposioty : IUserOrderRepository
    {
        private readonly BookDbContext _db;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<ApplicationUser> _userManager;

        public UserOrderReposioty(BookDbContext db,
            UserManager<ApplicationUser> userManager,
             IHttpContextAccessor httpContextAccessor)
        {
            _db = db;
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
        }
        public async Task ChangeOrderStatus(UpdateOrderStatusModel data)
        {
            var order = await _db.Orders.FindAsync(data.OrderId);
            if (order == null)
            {
                throw new InvalidOperationException($"Order with id: {data.OrderId} not found");
            }

            order.OrderStatusId = data.OrderStatusId;
            await _db.SaveChangesAsync();
        }

        public async Task<Order?> GetOrderById(int id)
        {
            return await _db.Orders.FindAsync(id);
        }

        public async Task<IEnumerable<OrderStatus>> GetOrderStatus()
        {
            return await _db.OrderStatuses.ToListAsync();
        }

        public async Task TogglePaymentStatus(int orderId)
        {
            var order = await _db.Orders.FindAsync(orderId);
            if (order == null)
            {
                throw new InvalidOperationException($"Order with id: {orderId} not found");
            }

            order.IsPaid = !order.IsPaid;
            await _db.SaveChangesAsync();
        }
        public async Task<IEnumerable<Order>> UserOrder(bool getAll = false)
        {
            var orders = _db.Orders
                .Include(x => x.OrderStatus)
                .Include(x => x.OrderDetails)
                .ThenInclude(x => x.Book)
                .ThenInclude(x => x.Genre)
                .AsQueryable();

            if (!getAll)
            {
                var userId = await GetUserId();
                if (string.IsNullOrEmpty(userId))
                {
                    throw new Exception("User is not logged-in");
                }

                orders = orders.Where(x => x.UserId == userId);
            }
            orders = orders.OrderByDescending(o => o.CreateDate);
            return await orders.ToListAsync();
        }

        private async Task<string> GetUserId()
        {
            var principal = _httpContextAccessor.HttpContext.User;
            var user = await _userManager.GetUserAsync(principal);
            return user?.Id;
        }
    }
}
