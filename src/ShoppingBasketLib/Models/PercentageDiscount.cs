using ShoppingBasketLib.Interfaces;

namespace ShoppingBasketLib.Models
{
    public class PercentageDiscount : IDiscount
    {
        public int Id { get; set; }
        public int ProductId { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
        public int DiscountedProductId { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
        public decimal Discount { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
    }
}
