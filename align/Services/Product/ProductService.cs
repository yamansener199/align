using align.Data;
using align.Data.Entities;
using align.Models.Product;
using align.Utils;
using Microsoft.EntityFrameworkCore;

namespace align.Services.Product
{
    public class ProductService : IProductService
    {
        private readonly AppDbContext _context;

        public ProductService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<ServiceResponse<AddProductResponseModel>> AddProduct(string productName, int productAmount)
        {
            var entity = new Data.Entities.Product
            {
                Amount = productAmount,
                Name = productName,
            };

            _context.Add(entity);

            await _context.SaveChangesAsync();

            return new ServiceResponse<AddProductResponseModel>()
            {
                Data = new AddProductResponseModel
                {
                    ProductAmount = productAmount,
                    ProductId = entity.Id,
                    ProductName = productName
                },
                ErrorMessage = null,
                StatusCode = 200
            };
        }

        public async Task<ServiceResponse<List<GetProductsResponseModel>>> GetProducts()
        {
            List<GetProductsResponseModel> getProductsResponseModels = new List<GetProductsResponseModel>();

            var entities = await _context.Products.Where(x => !x.IsDeleted).ToListAsync();

            foreach (var entity in entities)
            {
                getProductsResponseModels.Add(new GetProductsResponseModel
                {
                    ProductId = entity.Id,
                    AssignedProductAmount = SetAssignedProductAmount(entity),
                    ProductName = entity.Name,
                    UnAssignedProductAmount = SetUnAssignedProductAmount(entity)
                });
            }

            return new ServiceResponse<List<GetProductsResponseModel>>
            {
                Data = getProductsResponseModels,
                ErrorMessage = null,
                StatusCode = 200
            };
        }

        private int SetUnAssignedProductAmount(Data.Entities.Product entity)
        {
            // Will be implemented later
            return entity.Amount;
        }

        private int SetAssignedProductAmount(Data.Entities.Product entity)
        {
            // Will be implemented later
            return 0;
        }

        public async Task<ServiceResponse<UpdateProductResponseModel>> UpdateProduct(int productId, string productName, int unAssignedProductAmount)
        {
            var item = await _context.Products.Where(p => p.Id == productId && !p.IsDeleted).SingleOrDefaultAsync();

            if (item is null)
            {
                return new ServiceResponse<UpdateProductResponseModel>
                {
                    Data = null,
                    ErrorMessage = "Ürün bulunamadı.",
                    StatusCode = 404
                };
            }

            item.Name = productName;
            item.Amount = unAssignedProductAmount;

            await _context.SaveChangesAsync();

            return new ServiceResponse<UpdateProductResponseModel>()
            {
                Data = new UpdateProductResponseModel
                {
                    Name = productName,
                    UnAssignedAmount = unAssignedProductAmount
                },
                StatusCode = 200,
                ErrorMessage = null
            };
        }
    }
}
