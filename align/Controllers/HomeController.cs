using align.Models;
using align.Models.Product;
using align.Services.Product;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace align.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly IProductService _productService;
        public HomeController(ILogger<HomeController> logger, IProductService productService)
        {
            _logger = logger;
            _productService = productService;
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Orders()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Stocks()
        {
            var serviceResult = await _productService.GetProducts();

            if (serviceResult.isSuccess)
            {
                return View(serviceResult.Data);
            }

            throw new Exception("Error while fetching stocks");
        }

        public IActionResult Report()
        {
            return View();
        }

        public IActionResult Warehouse()
        {
            return View();
        }

        public IActionResult Settings()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("Home/AddProduct")]
        public async Task<IActionResult> AddProduct([FromBody] AddProductRequestModel request)
        {
            if(request.ProductName is null)
            {
                return StatusCode(403, "Ürün ismi boş olmamalı");
            }

            var result = await _productService.AddProduct(request.ProductName, request.Amount);

            if (result.isSuccess)
            {
                return Ok(result.Data);
            }
            else
            {
                return StatusCode(403, result.ErrorMessage);
            }
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateProduct([FromBody] UpdateProductRequestModel request)
        {
            if (request.ProductName is null || request.ProductId <= 0)
            {
                return StatusCode(403, "Ürün ismi veya ID hatalı");
            }

            var result = await _productService.UpdateProduct(request.ProductId, request.ProductName, request.UnAssignedProductAmount);

            if (result.isSuccess)
            {
                return Ok(result);
            }
            else
            {
                return StatusCode(403, result.ErrorMessage);
            }
        }

    }
}