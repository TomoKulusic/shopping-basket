using ShoppingBasket;
using ShoppingBasket.Utilities;
using ShoppingBasketTestApp.Utilities;
using System;

namespace ShoppingBasketTestApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello Shopping Basket App!");
            
            var shoppingBasket = new ShoppingBasketClass(GenerateProducts.GetProducts(), GenerateDiscounts.GetDiscounts());

            var products = shoppingBasket.GetProductList();
            var discounts = shoppingBasket.GetDiscounts();

            //var currentBasketStart = shoppingBasket.GetCurrentBasket();

            shoppingBasket.AddToBasket(2, 1);
            shoppingBasket.AddToBasket(2, 1);
            shoppingBasket.AddToBasket(3, 1);
            //shoppingBasket.AddToBasket(1, 1);
            //shoppingBasket.AddToBasket(1, 1);

            //var afterAddBasket = shoppingBasket.GetCurrentBasket();
        }
    }
}
