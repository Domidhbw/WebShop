using Microsoft.AspNetCore.Mvc;
using Website.Services;

namespace Website.Controllers
{
    public class CartController : Controller
    {
        private readonly ShoppingCartApi _apiServiceSH;
        private readonly ApiServiceProduct _apiServiceP;

        public CartController(ShoppingCartApi apiService, ApiServiceProduct apiServiceProduct)
        {
            _apiServiceSH = apiService;
            _apiServiceP = apiServiceProduct;
        }

        public async Task<IActionResult> Index()
        {
            // Fetch cart items from the API
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddToCart(int productId)
        {
            var cart = await _apiServiceSH.GetShoppingCartAsync();
            var product = (await _apiServiceP.GetProductsAsync())
                .FirstOrDefault(p => p.Id == productId);

            if (product != null)
            {
                cart.Products.Add(product);
                await _apiServiceSH.UpdateShoppingCartAsync(cart);
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
