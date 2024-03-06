namespace align.Data.Entities
{
    public sealed class Product : BaseSqlEntity
    {
        public string Name { get; set; }
        public int Amount { get; set; }
        public string RegionManagerId { get; set; }
        public User RegionManager { get; set; }
        public List<Order> Orders { get; set; }
    }
}
