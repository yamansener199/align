using align.Models.Product;
using align.Services.Product;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace align.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("/Product/Assign")]
        public async Task<ActionResult<AssignProductResponseModel>> Assign(AssignProductRequestModel request)
        {
            string userId = HttpContext.User.Identities.FirstOrDefault().Claims.FirstOrDefault().Value;

            if(userId is null)
            {
                return BadRequest("Session expired");
            }

            var result = await _productService.AssignProduct(request, userId);

            if (result.isSuccess)
            {
                return RedirectToAction("Stocks", "Home");
            }

            throw new Exception("Error while assigning product to a region manager");
        }
            
    }
}
