using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace align.Data.Entities
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public UserRole UserRole { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsDeleted { get; set; }
        public List<Order> Orders { get; set; } = new();
        public List<Product> Products { get; set; } = new();
        public List<ProductAssignHistory> ProductAssignHistoriesAsRegionManager { get; set; } = new();
        
        [NotMapped]
        public List<ProductAssignHistory> ProductAssignHistoriesAsAssigner { get; set; } = new();
    }

    public enum UserRole
    {
        Admin,
        RegionManager
    }
}
