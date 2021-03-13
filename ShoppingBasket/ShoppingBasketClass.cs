using ShoppingBasket.Interfaces;
using ShoppingBasket.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ShoppingBasket
{
    public class ShoppingBasketClass
    {
        public List<Product> _products;
        public List<IDiscount> _discounts;
        public Basket _basket = new Basket();

        public ShoppingBasketClass(List<Product> products)
        {
            _products = products;
            //_discounts = _discounts;     
        }

        public List<Product> GetProductList()
        {
            return _products;
        }

        public List<IDiscount> GetDiscounts()
        {
            return _discounts;
        }

        public void AddToBasket(int productId)
        {
            if (productId <= 0)
                throw new InvalidOperationException();

            _basket.Products.Add(GetItemFromListById(productId));

            //CalculateBasketDiscounts(Basket basket); // TODO

            CalculateCurrentBasketSum(_basket);

        }

        public void CalculateCurrentBasketSum(Basket basket)
        {
            basket.TotalSum = basket.Products.Select(x => x.Price).Sum();
        }

        public Basket GetCurrentBasket()
        {

            return _basket;
        }

        public Product GetItemFromListById(int productId)
        {
            var product = _products.SingleOrDefault(x => x.Id == productId);

            if (product is null)
                throw new Exception("Exception"); // todo refactoring

            return product;
        }

        public double GetCurrentBasketSum()
        {
            return _basket.Products.Select(x => x.Price).Sum();
        }
    }
}
