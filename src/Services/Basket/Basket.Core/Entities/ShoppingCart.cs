﻿namespace Basket.Core.Entities
{
    public class ShoppingCart
    {
        public string UserName { get; set; }

        public ShoppingCart(string userName)
        {
            UserName = userName;
        }
    }
}
