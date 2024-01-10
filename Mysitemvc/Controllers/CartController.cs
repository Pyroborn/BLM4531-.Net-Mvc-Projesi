using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Microsoft.Data.SqlClient;
using Mysitemvc.Entities;
using Mysitemvc.Models;
using Mysitemvc.Services;
using System.Data;
using System.Data.SqlClient;
using SqlCommand = Microsoft.Data.SqlClient.SqlCommand;
using SqlConnection = Microsoft.Data.SqlClient.SqlConnection;
using SqlTransaction = Microsoft.Data.SqlClient.SqlTransaction;
using Microsoft.AspNetCore.Http;
using System.Text.Json;


namespace Mysitemvc.Controllers
{
    public class CartController : Controller
    {
        private string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Test;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        private readonly ShoppingCart cart;
        public CartController()
        {
            cart = new ShoppingCart(); 
        }

            public List<CartItem> GetCartItems()
            {
                List<CartItem> cartItems = HttpContext.Session.GetObject<List<CartItem>>("Cart") ?? new List<CartItem>();
                return cartItems;
            }

            public void SaveCartItems(List<CartItem> cartItems)
            {
                HttpContext.Session.SetObject("Cart", cartItems);
            }

            [HttpPost]
            public ActionResult AddToCart(int productId, int quantity)
            {
                Db_ProductDao productDao = new Db_ProductDao();
                product_model product = productDao.GetProductById(productId);

                List<CartItem> cartItems = GetCartItems();

                CartItem existingItem = cartItems.FirstOrDefault(item => item.ProductId == productId);

                if (existingItem != null)
                {
                    existingItem.Quantity += quantity;
                }
                else
                {
                    CartItem newItem = new CartItem
                    {
                        ProductId = productId,
                        ProductName = product.Name,
                        ProductPrice = product.Price,
                        Quantity = quantity
                    };

                    cartItems.Add(newItem);
                }

                SaveCartItems(cartItems);

            return RedirectToAction("ShowDetails", "Product", new { id = productId });
        }

            public ActionResult ViewCart()
            {
                List<CartItem> cartItems = GetCartItems();
                return View(cartItems);
            }
        



        






        public ActionResult RemoveFromCart(int productId)
        {
            // Logic to remove product from the cart
            // Update the cart model accordingly
            // Redirect to cart view
            return RedirectToAction();
        }

        

    }
}
