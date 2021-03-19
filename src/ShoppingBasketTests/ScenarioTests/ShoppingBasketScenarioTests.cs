using ShoppingBasketLib.Interfaces;
using ShoppingBasketLib.Models;
using System.Collections.Generic;
using Xbehave;

namespace ShoppingBasketTests
{
    public class ShoppingBasketScenarioTests
    {
        public ShoppingBasketLib.ShoppingBasket _shoppingBasket;

        public List<Product> products = new List<Product>()
            {
                new Product
                {
                    Id = 1,
                    Name = "Milk",
                    Price = 1.15m,
                    Quantity = 50
                },
                new Product
                {
                    Id = 2,
                    Name = "Butter",
                    Price = 0.80m,
                    Quantity = 50,
                },
                new Product
                {
                    Id = 3,
                    Name = "Bread",
                    Price = 1.00m,
                    Quantity = 50
                }
            };

        public List<IDiscount> discounts = new List<IDiscount>() {
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


        [Scenario]
        public void Check1Milk1Butter1BreadTotalSum(Basket basket)
        {
            "Given that basket contains one bread one butter and one milk"
                .x(() => basket = new Basket()
                {
                    BasketItems = new List<BasketItem>()
                    {
                        new BasketItem
                        {
                            ProductId = 1,
                            Quantity = 1,
                        },
                        new BasketItem
                        {
                            ProductId = 2,
                            Quantity = 1
                        },
                        new BasketItem
                        {
                            ProductId = 3,
                            Quantity = 1
                        }
                    }
                });

            "And ShoppingCartClass"
                .x(() => _shoppingBasket = new ShoppingBasketLib.ShoppingBasket());

            "And when ShoppingCardClass is inicialized"
                .x(() => _shoppingBasket.Initialize(products, discounts));


            "When i calculte the basket total sum with applied discounts"
                .x(() => _shoppingBasket.ApplyDiscountsOnBasket(basket, discounts));

            "Then the total price is 2.95"
                .x(() => Xunit.Assert.Equal(2.95m, basket.TotalSum));
        }

        [Scenario]
        public void Check2Bread2ButterTotalSum(Basket basket)
        {
            "Given that basket contains one bread one butter and one milk"
                .x(() => basket = new Basket()
                {
                    BasketItems = new List<BasketItem>()
                    {
                        new BasketItem
                        {
                            ProductId = 2,
                            Quantity = 2,
                        },
                        new BasketItem
                        {
                            ProductId = 3,
                            Quantity = 2
                        },
                    }
                });

            "And ShoppingCartClass"
                .x(() => _shoppingBasket = new ShoppingBasketLib.ShoppingBasket());

            "And when ShoppingCardClass is inicialized"
                .x(() => _shoppingBasket.Initialize(products, discounts));


            "When i calculte the basket total sum with applied discounts"
                .x(() => _shoppingBasket.ApplyDiscountsOnBasket(basket, discounts));

            "Then the total price is 3.10, without any discounts applied"
                .x(() => Xunit.Assert.Equal(3.10m, basket.TotalSum));
        }

        [Scenario]
        public void Check4MilksWithDiscountTotalPrice(Basket basket)
        {
            "Given that basket contains one bread one butter and one milk"
                .x(() => basket = new Basket()
                {
                    BasketItems = new List<BasketItem>()
                    {
                        new BasketItem
                        {
                            ProductId = 1,
                            Quantity = 4,
                        },
                    }
                });

            "And ShoppingCartClass"
                .x(() => _shoppingBasket = new ShoppingBasketLib.ShoppingBasket());

            "And when ShoppingCardClass is inicialized"
                .x(() => _shoppingBasket.Initialize(products, discounts));


            "When i calculte the basket total sum with applied discounts"
                .x(() => _shoppingBasket.ApplyDiscountsOnBasket(basket, discounts));

            "Then the total price is 3.45, with discount applied"
                .x(() => Xunit.Assert.Equal(3.45m, basket.TotalSum));
        }

        [Scenario]
        public void Check2Butter1Bread8MilkDiscountTotalPrice(Basket basket)
        {
            "Given that basket contains one bread one butter and one milk"
                .x(() => basket = new Basket()
                {
                    BasketItems = new List<BasketItem>()
                    {
                        new BasketItem
                        {
                            ProductId = 1,
                            Quantity = 8,
                        },
                        new BasketItem
                        {
                            ProductId = 2,
                            Quantity = 2,
                        },
                        new BasketItem
                        {
                            ProductId = 3,
                            Quantity = 1,
                        },
                    }
                });

            "And ShoppingCartClass"
                .x(() => _shoppingBasket = new ShoppingBasketLib.ShoppingBasket());

            "And when ShoppingCardClass is inicialized"
                .x(() => _shoppingBasket.Initialize(products, discounts));


            "When i calculte the basket total sum with applied discounts"
                .x(() => _shoppingBasket.ApplyDiscountsOnBasket(basket, discounts));

            "Then the total price is 9.00, with discount applied"
                .x(() => Xunit.Assert.Equal(9.00m, basket.TotalSum));
        }
    }
}
