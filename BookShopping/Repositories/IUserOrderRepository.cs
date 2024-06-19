using System.Collections.Generic;
using System.Threading.Tasks;
using BookShopping.Models.DTOs;

namespace BookShopping.Repositories
{
    public interface IUserOrderRepository
    {
        Task<IEnumerable<Order>> UserOder(bool getAll=false);
        Task ChangeOrderStatus(UpdateOrderStatusModel data);
        Task TogglePaymentStatus(int orderId);
        Task<Order?> GetOrderById(int id);
        Task<IEnumerable<OrderStatus>> GetOrderStatus();
    }
}
