using Microsoft.Extensions.Logging;
using ShoppingBasket.Helpers;
using ShoppingBasketLib.Interfaces;
using ShoppingBasketLib.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShoppingBasketLib
{
    public class ShoppingBasket
    {
        public List<Product> _products;
        public List<IDiscount> _discounts;
        public List<Basket> _basket = new List<Basket>();
        public DiscountCalculator _discountCalculator;

        private readonly ILogger<ShoppingBasket> _logger;

        public ShoppingBasket(ILogger<ShoppingBasket> logger = null)
        {
            _logger = logger;
            _discountCalculator = new DiscountCalculator();
        }

        public void Initialize(List<Product> products, List<IDiscount> discounts)
        {
            _products = products;
            _discounts = discounts;
        }

        public List<Product> GetProductList()
        {
            return _products;
        }

        public List<IDiscount> GetDiscounts()
        {
            return _discounts;
        }

        public void AddToBasket(int productId, int userId)
        {
            if (productId <= 0)
                throw new InvalidOperationException();

            var product = _products.Where(x => x.Id == productId).FirstOrDefault();

            if (product is null)
                throw new ArgumentNullException("Cannot be null");

            var currentUserBasket = _basket.Where(x => x.UserId == userId).FirstOrDefault();

            if (currentUserBasket != null)
            {
                var checkIfProductIsAlreadyInBasket = currentUserBasket.BasketItems.Any(x => x.ProductId == product.Id);

                if (checkIfProductIsAlreadyInBasket)
                {
                    var basketItem = currentUserBasket.BasketItems.Where(x => x.ProductId == product.Id).FirstOrDefault();
                    basketItem.Quantity++;
                }
                else
                {
                    currentUserBasket.BasketItems.Add(
                        new BasketItem
                        {
                            BasketId = currentUserBasket.Id,
                            ProductId = product.Id,
                            DateCreated = DateTime.Now,
                            Quantity = currentUserBasket.BasketItems.Where(x => x.ProductId == product.Id).Count() + 1
                        }
                    );
                }

                ApplyDiscountsOnBasket(currentUserBasket, _discounts);

            }
            else
            {
                _basket.Add(CreateNewBasketWithNewProduct(product, userId));
                _logger?.LogInformation($"Product added to basket");
            }
        }

        public Basket CreateNewBasketWithNewProduct(Product product, int userId)
        {
            var newBasketId = _basket.Count() + 1;

            var newBasket = new Basket
            {
                Id = newBasketId,
                UserId = userId,
                BasketItems = new List<BasketItem>
                    {
                        new BasketItem
                        {
                            BasketId = newBasketId,
                            ProductId = product.Id,
                            DateCreated = DateTime.Now,
                            Quantity = 1
                        }
                    },
            };

            _logger?.LogInformation($"New basket created");

            return newBasket;
        }

        private void CalculateBasketSum(Basket newBasket)
        {
            var totalPricePerBasketItem = new List<decimal>();

            foreach (var basketItem in newBasket.BasketItems)
            {
                var basketItemProductPrice = _products.Where(x => x.Id == basketItem.ProductId).Select(y => y.Price).FirstOrDefault();

                totalPricePerBasketItem.Add(basketItemProductPrice * basketItem.Quantity);
            }

            newBasket.TotalSum = totalPricePerBasketItem.Sum();

            _logger?.LogInformation($"Calculating basket sum for BasketId: {newBasket.Id}");
        }

        public void ApplyDiscountsOnBasket(Basket basket, List<IDiscount> discounts)
        {
            //calculate the sum of the basket first
            CalculateBasketSum(basket);

            foreach (var basketItems in basket.BasketItems)
            {
                //reset basket item discounts
                basketItems.AppliedDiscounts.Clear();

                var _product = _products.Where(x => x.Id == basketItems.ProductId).FirstOrDefault();

                if (_product is null)
                    throw new ArgumentNullException("Cannot be null");

                var discountForProductList = discounts.Where(x => x.DiscountedProductId == _product.Id).ToList();

                if (discountForProductList != null)
                {
                    foreach (var _discount in discountForProductList)
                    {
                        var discountType = _discount.GetType();

                        if (discountType.Name == "RelationalDiscount")
                            _discountCalculator.CalculateRelationalDiscount(_discount, basket, _product);



                        if (discountType.Name == "QuantityDiscount")
                            _discountCalculator.CalculateQuantityDiscount(_discount, basket, _product);
                    }
                }
            }
        }

        public Basket GetCurrentUserBasket(int userId)
        {
            return _basket.FirstOrDefault(x => x.UserId == userId);
        }

        public Product GetItemFromListById(int productId)
        {
            var product = _products.FirstOrDefault(x => x.Id == productId);

            if (product is null)
                throw new ArgumentNullException("Value cannot be null");

            return product;
        }

        public string GetBasketStatusTxtForUser(int userId)
        {
            var userBasket = _basket.Where(x => x.UserId == userId).FirstOrDefault();

            var stringBuilder = new StringBuilder();
            stringBuilder.AppendLine($"Item\tQuantity");

            foreach (var basketItem in userBasket.BasketItems)
            {
                var product = _products.SingleOrDefault(x => x.Id == basketItem.ProductId);
                var appliedDiscountForProduct = basketItem.AppliedDiscounts.Where(x => x.DiscountedProductId == basketItem.ProductId).FirstOrDefault();

                stringBuilder.AppendLine($"{product.Name}\t{basketItem.Quantity}");
            }
            //TODO
            stringBuilder.AppendLine($"Discounts:\t{userBasket.TotalSum}");

            stringBuilder.AppendLine($"Total:\t{userBasket.TotalSum}");

            return stringBuilder.ToString();
        }
    }
}
