namespace align.Data.Entities
{
    public sealed class Product : BaseSqlEntity
    {
        public string Name { get; set; } = null!;
        public int Amount { get; set; } // Represents unassigned stock count of product
        public List<User> RegionManagers { get; set; } = new();
        public List<Order> Orders { get; set; } = new();
        public List<ProductAssignHistory> ProductAssignHistories { get; set; } = new();
    }
}
