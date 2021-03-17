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

            //first check
            shoppingBasket.AddToBasket(1, 1);
            shoppingBasket.AddToBasket(2, 1);
            shoppingBasket.AddToBasket(3, 1);

            //secondCheck
            shoppingBasket.AddToBasket(2, 2);
            shoppingBasket.AddToBasket(2, 2);
            shoppingBasket.AddToBasket(3, 2);
            shoppingBasket.AddToBasket(3, 2);

            //thirdCheck
            shoppingBasket.AddToBasket(1, 3);
            shoppingBasket.AddToBasket(1, 3);
            shoppingBasket.AddToBasket(1, 3);
            shoppingBasket.AddToBasket(1, 3);

            //forthCheck
            shoppingBasket.AddToBasket(2, 4);
            shoppingBasket.AddToBasket(2, 4);
            shoppingBasket.AddToBasket(3, 4);
            shoppingBasket.AddToBasket(1, 4);
            shoppingBasket.AddToBasket(1, 4);
            shoppingBasket.AddToBasket(1, 4);
            shoppingBasket.AddToBasket(1, 4);
            shoppingBasket.AddToBasket(1, 4);
            shoppingBasket.AddToBasket(1, 4);
            shoppingBasket.AddToBasket(1, 4);
            shoppingBasket.AddToBasket(1, 4);


            var afterAddBasket = shoppingBasket.GetCurrentUserBasket(1);
            var afterAddBasketSecondUser = shoppingBasket.GetCurrentUserBasket(2);
            var afterAddBasketThirdUser = shoppingBasket.GetCurrentUserBasket(3);
            var afterAddBasketTForthUser = shoppingBasket.GetCurrentUserBasket(4);

            Console.WriteLine(shoppingBasket.GetBasketStatusTxtForUser(4));
        }
    }
}
