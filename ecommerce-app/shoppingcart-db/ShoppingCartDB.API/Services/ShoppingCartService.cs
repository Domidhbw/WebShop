using ShoppingCartDB.API.Models;
using System.Text.Json;

namespace ShoppingCartDB.API.Services
{

    public class ShoppingCartService
    {
        private readonly string _filePath = Path.Combine(AppContext.BaseDirectory, "Data", "ShoppingCartData.json");

        public List<ShoppingCart> GetShoppingCarts()
        {
            // ✅ If file does not exist, return default test data
            if (!File.Exists(_filePath))
            {
                return GetDefaultCart();
            }

            // ✅ If file exists but is empty, return default test data
            var json = File.ReadAllText(_filePath);
            if (string.IsNullOrWhiteSpace(json))
            {
                return GetDefaultCart();
            }

            try
            {
                return JsonSerializer.Deserialize<List<ShoppingCart>>(json) ?? GetDefaultCart();
            }
            catch (JsonException)
            {
                // ✅ Handle corrupted JSON case
                Console.WriteLine("Error: Invalid JSON format in ShoppingCartData.json");
                return GetDefaultCart();
            }
        }

        public ShoppingCart? GetShoppingCartByUser(string username)
        {
            var carts = GetShoppingCarts();
            return carts.FirstOrDefault(cart => cart.UserName.Equals(username, StringComparison.OrdinalIgnoreCase));
        }

        public void SaveShoppingCarts(List<ShoppingCart> carts)
        {
            var json = JsonSerializer.Serialize(carts, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(_filePath, json);
        }

        public void AddOrUpdateShoppingCart(ShoppingCart cart)
        {
            var carts = GetShoppingCarts();
            var existingCart = carts.FirstOrDefault(c => c.UserName.Equals(cart.UserName, StringComparison.OrdinalIgnoreCase));

            if (existingCart != null)
            {
                carts.Remove(existingCart);
            }

            carts.Add(cart);
            SaveShoppingCarts(carts);
        }

        private List<ShoppingCart> GetDefaultCart()
        {
            return new List<ShoppingCart>
        {
            new ShoppingCart
            {
                UserName = "test",
                LastPaymentMethod = "test",
                LastShippedToAddress = "test",
                Products = new List<Product>()
            }
        };
        }
    }
}