using ShoppingBasket.Interfaces;
using ShoppingBasket.Models;
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
                 //new RelationalDiscount()
                 //{
                 //   Id = 2,
                 //   ProductId = 1,
                 //   DiscountedProductId = 2,
                 //   ProductRequiredAmount = 3,
                 //   Discount = 0.70m
                 //},
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
