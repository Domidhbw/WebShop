using Website.Models;

namespace Website.Services
{
    public class ProductService
    {
        private readonly List<Product> _products = new()
        {
            new Product { Id = 1, Name = "Product 1", Price = 10.99m },
            new Product { Id = 2, Name = "Product 2", Price = 20.99m },
            new Product { Id = 3, Name = "Product 3", Price = 30.99m },
            new Product { Id = 4, Name = "Product 4", Price = 40.99m },
            new Product { Id = 5, Name = "Product 5", Price = 50.99m }
        };

        public List<Product> GetProducts(string searchTerm = "")
        {
            if (string.IsNullOrEmpty(searchTerm))
                return _products;

            return _products
                .Where(p => p.Name.Contains(searchTerm, StringComparison.OrdinalIgnoreCase))
                .ToList();
        }
    }
}

