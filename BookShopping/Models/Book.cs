using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookShopping.Models
{
    public class Book
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(40)]
        public string Name { get; set; }

        [Required]
        [MaxLength(40)]
        public string Author{ get; set; }
        [Required]
        [StringLength(50)]
        public double Price { get; set; }
        public string Image { get; set; }
        [Required]
        public int GenreId { get; set; }
        public Genre Genre { get; set; }
        public List<OrderDetail> OrderDetails { get; set; }
        public List<CartDetail> CartDetails { get; set; }
        public Stock Stock { get; set; }

        [NotMapped]
        public string GenreName { get; set; }
        [NotMapped]
        public int Quantity { get; set; }
    }
}
