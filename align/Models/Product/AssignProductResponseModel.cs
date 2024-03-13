namespace align.Models.Product
{
    public class AssignProductResponseModel
    {
        public string RegionManagerId { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int AssignedProductAmount { get; set; }
    }
}
