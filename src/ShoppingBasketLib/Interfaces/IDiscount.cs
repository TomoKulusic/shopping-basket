namespace ShoppingBasketLib.Interfaces
{
    public interface IDiscount
    {
        int Id { get; set; }
        int DiscountedProductId { get; set; }
        decimal Discount { get; set; }
    }
}
