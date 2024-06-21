namespace BookShopping.Models.DTOs
{
    public class UserOrderModel
    {
        public IEnumerable<Order> Orders { get; set; }
        public Ranks Rank { get; set; }
    }
}
