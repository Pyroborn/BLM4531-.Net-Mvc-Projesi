﻿using System.ComponentModel.DataAnnotations;

namespace Mysitemvc.Models
{


    public class CartItem
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal ProductPrice { get; set; }
        public int Quantity { get; set; }
    }



}
