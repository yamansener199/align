namespace align.Data.Entities
{
    public class ProductAssignHistory : BaseSqlEntity
    {
        public int ProductId { get; set; }
        public string RegionManagerId { get; set; }
        public string AssignerUserId { get; set; }
        public User RegionManager { get; set; }
        public User AssignerUser { get; set; }
        public Product Product { get; set; }
        public int AssignedProductAmount { get; set; }
    }
}
