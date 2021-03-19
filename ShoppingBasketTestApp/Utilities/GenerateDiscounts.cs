using ShoppingBasketLib.Interfaces;
using ShoppingBasketLib.Models;
using System.Collections.Generic;

namespace ShoppingBasket.Utilities
{
    public static class GenerateDiscounts
    {
        public static List<IDiscount> GetDiscounts()
        {
            return new List<IDiscount>()
            {
                 new RelationalDiscount()
                 {
                    Id = 1,
                    ProductId = 2,
                    DiscountedProductId = 3,
                    ProductRequiredAmount = 2,
                    Discount = 0.50m
                 },
                 new QuantityDiscount()
                 {
                    Id = 1,
                    DiscountedProductId = 1,
                    RequiredDiscountAmount = 3,
                    Discount = 0,
                    DiscountedAmout = 1,
                 }
            };
        }
    }
}
