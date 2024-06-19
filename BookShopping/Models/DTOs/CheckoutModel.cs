using System.ComponentModel.DataAnnotations;

namespace BookShopping.Models.DTOs
{
    public class CheckoutModel
    {
        [Required]
        [MaxLength(30)]
        public string? Name { get; set; }
       
        [EmailAddress]
        public string? Email { get; set; }
        [Required]
        public string MobileNumber { get; set; }
        [Required]
        [MaxLength(200)]
        public string Address { get; set; }
        [Required]
        [MaxLength(30)]
        public string PaymentMethod { get; set; }
    }
}
