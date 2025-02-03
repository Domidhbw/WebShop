using Microsoft.AspNetCore.Mvc;
using Website.Services;

namespace Website.Controllers
{
    public class ProductsController : Controller
    {
        private readonly ApiServiceProduct _apiService;

        public ProductsController(ApiServiceProduct apiServiceProduct)
        {
            _apiService = apiServiceProduct;
        }

        public async Task<IActionResult> Index(string searchTerm)
        {
            Console.WriteLine("VIEW");
            var products = await _apiService.GetProductsAsync();
            Console.WriteLine(products);
            if (!string.IsNullOrEmpty(searchTerm))
            {
                products = products
                    .Where(p => p.Name.Contains(searchTerm, StringComparison.OrdinalIgnoreCase))
                    .ToList();
            }
            return View(products);
        }

    }
}
