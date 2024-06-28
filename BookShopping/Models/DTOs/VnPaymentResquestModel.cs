namespace BookShopping.Models.DTOs
{
    public class VnPaymentResquestModel
    {
        public int OrderId { get; set; }
        public string Fullname { get; set; }
        public string Address { get; set; }
        public double Amount { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
