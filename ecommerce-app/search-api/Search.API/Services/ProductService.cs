using Search.API.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
namespace Search.API.Services
{
    public class ProductService
    {
        private readonly string _filePath = Directory.GetCurrentDirectory() + "\\Data\\products.json";

        public List<Product> GetProducts()
        {
            Console.WriteLine(Directory.GetCurrentDirectory());
            Console.WriteLine(_filePath);
            if (!File.Exists(_filePath))
            {
                return new List<Product>();
            }

            var json = File.ReadAllText(_filePath);
            return JsonSerializer.Deserialize<List<Product>>(json) ?? new List<Product>();
        }

        public void AddProduct(Product product)
        {
            var products = GetProducts();
            products.Add(product);
            SaveProducts(products);
        }

        private void SaveProducts(List<Product> products)
        {
            var json = JsonSerializer.Serialize(products);
            File.WriteAllText(_filePath, json);
        }
    }
}