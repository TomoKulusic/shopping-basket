using ShoppingBasket;
using ShoppingBasketTestApp.Utilities;
using System;

namespace ShoppingBasketTestApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello Shopping Basket App!");
            
            var shoppingBasket = new ShoppingBasketClass(GenerateProducts.GetProducts());

            var products = shoppingBasket.GetProductList();

            var currentBasketStart = shoppingBasket.GetCurrentBasket();

            shoppingBasket.AddToBasket(1);
            shoppingBasket.AddToBasket(1);
            shoppingBasket.AddToBasket(2);

            var afterAddBasket = shoppingBasket.GetCurrentBasket();
        }
    }
}
