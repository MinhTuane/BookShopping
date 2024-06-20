using Microsoft.AspNetCore.Identity;

namespace BookShopping.Models.User
{
    public class ApplicationUser : IdentityUser
    {
        public byte[] AvatarImage { get; set; }
        public double TotalSpend { get; set; }
        public Ranks Rank { get; set; }
    }
}
