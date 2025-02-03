using Website.Models;

namespace Website.Services
{
    public class ApiServiceProduct
    {
        private readonly HttpClient _httpClient;

        public ApiServiceProduct(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<Product>> GetProductsAsync()
        {
            var response = await _httpClient.GetAsync("/products");
            response.EnsureSuccessStatusCode();

            // Log the raw response
            var rawResponse = await response.Content.ReadAsStringAsync();
            Console.WriteLine("API Response: " + rawResponse);

            return await response.Content.ReadFromJsonAsync<List<Product>>();
        }

        public async Task AddToCartAsync(int productId)
        {
            var response = await _httpClient.PostAsJsonAsync("/api/cart", new { ProductId = productId });
            response.EnsureSuccessStatusCode();
        }


    }
}

