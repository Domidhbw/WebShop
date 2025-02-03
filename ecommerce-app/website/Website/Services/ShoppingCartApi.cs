using System.Net.Http;
using Website.Models;

namespace Website.Services
{
    public class ShoppingCartApi
    {
        private readonly HttpClient _httpClient;

        public ShoppingCartApi(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        // Services/ApiService.cs
        public async Task<ShoppingCart> GetShoppingCartAsync(string username = "default")
        {
            var response = await _httpClient.GetAsync($"/shoppingcart/{username}");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<ShoppingCart>();
        }

        public async Task UpdateShoppingCartAsync(ShoppingCart cart)
        {
            var response = await _httpClient.PostAsJsonAsync("/shoppingcart", cart);
            response.EnsureSuccessStatusCode();
        }
    }
}
