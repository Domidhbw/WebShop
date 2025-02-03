using ShoppingCart.API.Models;
using System.Text.Json;

namespace ShoppingCart.API.Services
{
    public class ShoppingCartService
    {
        private readonly string _filePath = Path.Combine(AppContext.BaseDirectory, "Data", "ShoppingCartData.json");

        public List<ShoppingCart.API.Models.ShoppingCart> GetShoppingCarts()
        {
            if (!File.Exists(_filePath))
            {
                return new List<Models.ShoppingCart>
            {
                new Models.ShoppingCart
                {
                    UserName = "test",
                    LastPaymentMethod = "test",
                    LastShippedToAddress = "test",
                    Products = new List<Product>()
                }
            };
            }

            var json = File.ReadAllText(_filePath);
            return JsonSerializer.Deserialize<List<Models.ShoppingCart>>(json) ?? new List<Models.ShoppingCart>();
        }

        public Models.ShoppingCart? GetShoppingCartByUser(string username)
        {
            var carts = GetShoppingCarts();
            return carts.FirstOrDefault(cart => cart.UserName.Equals(username, StringComparison.OrdinalIgnoreCase));
        }

        public void SaveShoppingCarts(List<Models.ShoppingCart> carts)
        {
            var json = JsonSerializer.Serialize(carts, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(_filePath, json);
        }

        public void AddOrUpdateShoppingCart(Models.ShoppingCart cart)
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
    }
}
