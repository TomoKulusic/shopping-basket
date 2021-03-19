using ShoppingBasketLib.Interfaces;

namespace ShoppingBasketLib.Models
{
    public class RelationalDiscount : IDiscount
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int DiscountedProductId { get; set; }
        public int ProductRequiredAmount { get; set; }
        public decimal Discount { get; set; }
    }
}
