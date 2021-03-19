using ShoppingBasketLib.Interfaces;

namespace ShoppingBasketLib.Models
{
    public class QuantityDiscount : IDiscount
    {
        public int Id { get; set; }
        public int DiscountedProductId { get; set; }
        public decimal Discount { get; set; }
        public int RequiredDiscountAmount { get; set; }
        public int DiscountedAmout { get; set; }
    }
}
