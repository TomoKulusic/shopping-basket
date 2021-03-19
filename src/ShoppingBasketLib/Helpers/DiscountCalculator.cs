using Microsoft.Extensions.Logging;
using ShoppingBasketLib.Interfaces;
using ShoppingBasketLib.Models;
using System;
using System.Linq;

namespace ShoppingBasket.Helpers
{
    public class DiscountCalculator
    {
        private readonly ILogger<DiscountCalculator> _logger;
        public DiscountCalculator(ILogger<DiscountCalculator> logger = null)
        {
            _logger = logger;
        }
        public void CalculateRelationalDiscount(IDiscount discount, Basket basket, Product product)
        {
            if (basket is null)
                throw new ArgumentNullException("Basket value cannot be null", nameof(basket));

            if (product is null)
                throw new ArgumentNullException("Product value cannot be null", nameof(product));

            var currentDiscount = (RelationalDiscount)discount;

            var currentBasketItem = basket.BasketItems.Where(x => x.ProductId == currentDiscount.ProductId).FirstOrDefault();
            var isDiscountValid = currentDiscount.ProductRequiredAmount.Equals(currentBasketItem.Quantity);

            if (isDiscountValid)
            {
                var productStandardPrice = product.Price;
                var priceWithDiscount = product.Price * discount.Discount;

                basket.TotalSum = basket.TotalSum - (productStandardPrice - priceWithDiscount);
                currentBasketItem.AppliedDiscounts.Add(discount);

                _logger?.LogInformation($"Discount of {discount.Discount}% applied on {product.Name}");
            }
        }

        public void CalculateQuantityDiscount(IDiscount discount, Basket basket, Product product)
        {
            if (basket is null)
                throw new ArgumentNullException("Basket value cannot be null", nameof(basket));

            if (product is null)
                throw new ArgumentNullException("Product value cannot be null", nameof(product));

            var currentDiscount = (QuantityDiscount)discount;
            var currentBasketItem = basket.BasketItems.Where(x => x.ProductId == currentDiscount.DiscountedProductId).FirstOrDefault();
            var productRequirementCount = currentDiscount.RequiredDiscountAmount;

            if (productRequirementCount < currentBasketItem.Quantity)
            {
                var currentDiscountAppliedTimes = currentBasketItem.Quantity / currentDiscount.RequiredDiscountAmount;
                var itemAmountThatShouldBeDiscountedTotalPrice = product.Price * currentDiscountAppliedTimes;
                var priceWithDiscount = product.Price * discount.Discount;

                basket.TotalSum = basket.TotalSum - (itemAmountThatShouldBeDiscountedTotalPrice - priceWithDiscount);
                currentBasketItem.AppliedDiscounts.Add(discount);

                _logger?.LogInformation($"Discount of {discount.Discount}% applied on {product.Name}");

            }
        }
    }
}
