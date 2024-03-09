namespace align.Models.Product
{
    public class GetProductsResponseModel
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int UnAssignedProductAmount { get; set; }
        public int AssignedProductAmount { get; set; }
    }
}
