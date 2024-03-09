namespace align.Models.Product
{
    public class UpdateProductRequestModel
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int UnAssignedProductAmount { get; set; }
    }
}
