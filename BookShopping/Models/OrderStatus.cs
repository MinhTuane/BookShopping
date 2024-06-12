using System.ComponentModel.DataAnnotations;

namespace BookShopping.Models
{
    public class OrderStatus
    {
        public int Id { get; set; }
        [Required]
        public string StatusName { get; set; }
    }
}
