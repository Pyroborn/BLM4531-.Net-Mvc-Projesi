using System.ComponentModel.DataAnnotations;

namespace Mysitemvc.Models
{
    public class Cart
    {
        public class ShoppingCart 
        {
            [Key]
            public int ShoppingCartId { get; set; }
            public virtual UserProfile UserProfile { get; set; }
            public virtual ICollection<CartItem> CartItems { get; set; }
        }

        public class CartItem
        {
            [Key]
            public int CartItemId { get; set; }
            public virtual ShoppingCart ShoppingCart { get; set; }
            public virtual product_model product { get; set; }
            public int Quantity { get; set; }
        }

    }
}
