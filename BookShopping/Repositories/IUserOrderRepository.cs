namespace BookShopping.Repositories
{
    public interface IUserOrderRepository
    {
        Task<IEnumerable<Order>> UserOrder(bool getAll = false);
        Task ChangeOrderStatus(UpdateOrderStatusModel data);
        Task TogglePaymentStatus(int orderId);
        Task<Order> GetOrderById(int id);
        Task<IEnumerable<OrderStatus>> GetOrderStatus();
    }
}
