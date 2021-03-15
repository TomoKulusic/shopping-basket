namespace ShoppingBasket.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }
        public bool IsDiscountApplied { get; set; }
        public string DiscountMessage { get; set; }
    }
}
