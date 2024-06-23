namespace BookShopping.Models.DTOs
{
    public class UserMessageModel
    {
        public string UserId { get; set; }
        public List<Message> Messages { get; set; }
    }
}
