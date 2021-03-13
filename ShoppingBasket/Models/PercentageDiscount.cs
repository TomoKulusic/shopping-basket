using ShoppingBasket.Interfaces;

namespace ShoppingBasket.Models
{
    public class PercentageDiscount : IDiscount
    {
        public int Id { get; set; }
        public decimal Percentage { get; set; }
    }
}
