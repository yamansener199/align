namespace align.Data.Entities
{
    public sealed class Order : BaseSqlEntity
    {
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public string RegionManagerId { get; set; }
        public User RegionManager { get; set; }
        public string AccountNumber { get; set; }
        public string DoctorFullName { get; set; }
    }
}
