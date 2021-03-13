using ShoppingBasket.Interfaces;
using System.Collections.Generic;

namespace ShoppingBasket.Models
{
    public class Basket
    {
        public Basket()
        {
            Products = new List<Product>();
            TotalSum = 0;
        }

        public List<Product> Products { get; set; }
        //public List<IDiscount> Discounts { get; set; }
        public double TotalSum { get; set; }
    }
}
