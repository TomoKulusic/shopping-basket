using System;
using System.Collections.Generic;
using System.Text;

namespace ShoppingBasket.Models
{
    public class Basket
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public decimal TotalSum { get; set; }
        public List<BasketItem> BasketItems { get; set; }

        public Basket()
        {
            BasketItems = new List<BasketItem>();
        }
    }
}
