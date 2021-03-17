using ShoppingBasket.Interfaces;
using System;
using System.Collections.Generic;

namespace ShoppingBasket.Models
{
    public class BasketItem
    {
        public int BasketId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public DateTime DateCreated { get; set; }
        public List<IDiscount> AppliedDiscounts { get; set; }

        public BasketItem()
        {
            AppliedDiscounts = new List<IDiscount>();
        }
    }
}
