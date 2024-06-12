using System.ComponentModel.DataAnnotations;

namespace BookShopping.Models
{
    public class Book
    {
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string Name { get; set; }
        public double Price { get; set; }
        public string Image { get; set; }
        [Required]
        public int GenId { get; set; }
        public Genre Genre;
        public List<OrderDetail> OrderDetails { get; set; }
        public List<CartDetail> CartDetails { get; set; }
    }
}
