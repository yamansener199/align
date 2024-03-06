using Microsoft.AspNetCore.Identity;

namespace align.Data.Entities
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public UserRole UserRole { get; set; }
        public List<Order> Orders { get; set; }
        public List<Product> Products { get; set; }
    }

    public enum UserRole
    {
        Admin,
        RegionManager
    }
}
