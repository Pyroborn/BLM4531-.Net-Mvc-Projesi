using Microsoft.AspNetCore.Mvc;
using Mysitemvc.Models;
using Mysitemvc.Services;
// yeni repository oluşturulur ve view ile çağırılır
namespace Mysitemvc.Controllers
{
    public class productController : Controller
    {
        public IActionResult Index()
        {
            Db_ProductDao db_Productdao = new Db_ProductDao();
            return View(db_Productdao.GetAllProducts());
        }


        public IActionResult SearchResults(string searchTerm)
        {
            Db_ProductDao products = new Db_ProductDao();
            List<product_model> productList = products.SearchProducts(searchTerm);
            return View("index", productList);
        }
        public IActionResult SearchForm(int id) 
        {
            return View();
        }
    }
}