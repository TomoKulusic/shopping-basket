using ShoppingBasket.Models;
using System.Collections.Generic;
using System.Linq;

namespace ShoppingBasket.Helpers
{
    public static class DiscountHelper
    {
        public static void CalculateRelationalDiscounts(List<RelationalDiscount> relationalDiscountList, Basket basket)
        {
            //check if there is a applicable product for relational discount
            var godHelpMe = basket.Products.Any(x => relationalDiscountList.Any(y => y.DiscountedProductId.Equals(x.Id)));

            //apply discounts if condition above is true
            if (godHelpMe)
            {
                var _products = basket.Products.Where(x => relationalDiscountList.Any(y => y.DiscountedProductId.Equals(x.Id))).ToList();

                //Need to add a check if discount can be applied multiple times
                //for example, to get 50% off of a bread, you need to have 2 butters in basket
                //but what if you have 4 butters and 2 breads?

                foreach (var discount in relationalDiscountList)
                {
                    foreach (var _product in _products)
                    {
                        //check if product relates to discount
                        var isRightProduct = _product.Id == discount.DiscountedProductId;

                        if (isRightProduct)
                        {
                            //check if any product contains valid amount for discount
                            var getTopProductRequirementCount = basket.Products.Where(x => x.Id == discount.ProductId).ToList().Count;
                            var validity = discount.ProductRequiredAmount.Equals(getTopProductRequirementCount);

                            if (validity)
                            {
                                var oldPrice = _product.Price;
                                _product.Price = _product.Price * discount.DiscountPercentage;
                                _product.IsDiscountApplied = true;
                                _product.DiscountMessage = $"By buying two butters, you get 50% discount for Bread. {_product.Price}%";
                            }
                        }
                    }
                }
            }
        }
    }
}
