using align.Models.Product;
using align.Utils;

namespace align.Services.Product
{
    public interface IProductService
    {
        Task<ServiceResponse<AddProductResponseModel>> AddProduct(string productName, int productAmount);
        Task<ServiceResponse<List<GetProductsResponseModel>>> GetProducts();
        Task<ServiceResponse<UpdateProductResponseModel>> UpdateProduct(int productId, string productName, int unAssignedProductAmount);
    }
}
