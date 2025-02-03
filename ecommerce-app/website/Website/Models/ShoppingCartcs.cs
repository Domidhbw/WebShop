namespace Website.Models
{
    public class ShoppingCart
    {
        public string UserName { get; set; } = "default";
        public List<Product> Products { get; set; } = new List<Product>();
        public string LastPaymentMethod { get; set; }
        public string LastShippedToAddress { get; set; }
    }
}
