using Microsoft.AspNetCore.Mvc;
using Website.Services;

namespace Website.Controllers
{
    public class CartController : Controller
    {
        private readonly ApiServiceProduct _apiService;

        public CartController(ApiServiceProduct apiService)
        {
            _apiService = apiService;
        }

        public async Task<IActionResult> Index()
        {
            // Fetch cart items from the API
            return View();
        }
    }
}
