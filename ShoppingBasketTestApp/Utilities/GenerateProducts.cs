using ShoppingBasket.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShoppingBasketTestApp.Utilities
{
    public static class GenerateProducts
    {
        public static List<Product> GetProducts()
        {
            return new List<Product>()
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
        }
    }
}
