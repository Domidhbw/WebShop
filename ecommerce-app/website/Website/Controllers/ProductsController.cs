using Microsoft.AspNetCore.Mvc;
using Website.Services;

namespace Website.Controllers
{
    public class ProductsController : Controller
    {
        private readonly ProductService _productService;

        public ProductsController(ProductService productService)
        {
            _productService = productService;
        }

        public IActionResult Index(string searchTerm)
        {
            var products = _productService.GetProducts(searchTerm);
            return View(products);
        }
    }
}
