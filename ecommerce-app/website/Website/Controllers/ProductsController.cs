using Microsoft.AspNetCore.Mvc;
using Website.Services;

namespace Website.Controllers
{
    public class ProductsController : Controller
    {
        private readonly ApiServiceProduct _apiServiceProduct;

        public ProductsController(ApiServiceProduct apiServiceProduct)
        {
            _apiServiceProduct = apiServiceProduct;
        }

        public async Task<IActionResult> Index(string searchTerm)
        {
            var products = await _apiServiceProduct.GetProductsAsync();
            if (!string.IsNullOrEmpty(searchTerm))
            {
                products = products
                    .Where(p => p.Name.Contains(searchTerm, StringComparison.OrdinalIgnoreCase))
                    .ToList();
            }
            return View(products);
        }

        [HttpPost]
        public async Task<IActionResult> AddToCart(int productId)
        {
            await _apiServiceProduct.AddToCartAsync(productId);
            return RedirectToAction(nameof(Index));
        }
    }
}
