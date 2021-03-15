using ShoppingBasket.Helpers;
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

        public ShoppingBasketClass(List<Product> products, List<IDiscount> discounts)
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

        public void AddToBasket(int productId)
        {
            if (productId <= 0)
                throw new InvalidOperationException();

            _basket.Products.Add(GetItemFromListById(productId));

            var test = ApplyDiscountsOnBasket(_basket, _discounts);

        }

        public Basket ApplyDiscountsOnBasket(Basket basket, List<IDiscount> discounts)
        {
            //check if discount exist for products in basket
            var _discounts = discounts.Where(x => basket.Products.Any(product => product.Id.Equals(x.ProductId))).ToList();

            if (_discounts is null)
                return basket;

            //checkForRelationalDiscount
            var relationalDiscounts = _discounts.OfType<RelationalDiscount>().ToList();

            if (relationalDiscounts != null)
                DiscountHelper.CalculateRelationalDiscounts(relationalDiscounts, basket);

            //check for single purchase discount TODO


            CalculateCurrentBasketSum(basket);
            return basket;
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
