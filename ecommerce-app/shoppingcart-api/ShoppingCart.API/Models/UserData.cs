namespace ShoppingCart.API.Models
{
    public class UserData
    {
        public string UserName { get; set; }
        public string LastPaymentMethod { get; set; }
        public string LastShippedToAddress { get; set; }
        public List<Product> BoughtProducts { get; set; } = new List<Product>();
    }
}

