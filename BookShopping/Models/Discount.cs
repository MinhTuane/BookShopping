using System.ComponentModel.DataAnnotations;

namespace BookShopping.Models
{
    public class Discount
    {
        public int Id { get; set; }
        [Required]
        [RegularExpression(@"^[A-Z0-9]{10}$")]
        public string Code { get; set; }
        [Required]
        [Range(0, 1, ErrorMessage = "Rate must be between 0 and 1")]
        public double Rate { get; set; }
    }
}
