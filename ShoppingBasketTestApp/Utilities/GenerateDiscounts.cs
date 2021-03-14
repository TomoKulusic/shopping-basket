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
                    DiscountPercentage = 0.50
                 }
            };
        }
    }
}
