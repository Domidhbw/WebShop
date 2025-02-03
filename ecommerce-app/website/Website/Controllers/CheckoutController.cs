// Controllers/CheckoutController.cs
using Microsoft.AspNetCore.Mvc;
using Website.Services;

public class CheckoutController : Controller
{
    private readonly ShoppingCartApi _apiService;

    public CheckoutController(ShoppingCartApi apiService)
    {
        _apiService = apiService;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var cart = await _apiService.GetShoppingCartAsync();
        return View(cart);
    }

    [HttpPost]
    public async Task<IActionResult> ProcessCheckout(
        string address,
        string paymentMethod)
    {
        var cart = await _apiService.GetShoppingCartAsync();

        cart.LastShippedToAddress = address;
        cart.LastPaymentMethod = paymentMethod;

        await _apiService.UpdateShoppingCartAsync(cart);

        // Process payment and shipping logic here

        return RedirectToAction("Confirmation");
    }

    public IActionResult Confirmation()
    {
        return View();
    }
}
