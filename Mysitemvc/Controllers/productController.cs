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

        public IActionResult showDetails(int id) 
        {
            Db_ProductDao product = new Db_ProductDao();
            product_model foundProduct = product.GetProductById(id);
            return View(foundProduct);
        }

        public IActionResult ShowOneProductJSON(int id) 
        { 
            Db_ProductDao product = new Db_ProductDao();
            return Json(product.GetProductById(id));
        }
        public IActionResult edit(int id)
        {
            Db_ProductDao product = new Db_ProductDao();
            product_model foundProduct = product.GetProductById(id);
            return View("ShowEdit", foundProduct);
        }

        public IActionResult ProcessEdit(product_model product)
        {
            Db_ProductDao products = new Db_ProductDao();
            products.Update(product);
            return View("index", products.GetAllProducts());
        }
        public IActionResult ProcessEditReturnPartial(product_model product)
        {
            Db_ProductDao products = new Db_ProductDao();
            products.Update(product);
            return PartialView("_productCard", product);
        }
        public IActionResult SearchResults(string searchTerm)
        {
            Db_ProductDao products = new Db_ProductDao();
            List<product_model> productList = products.SearchProducts(searchTerm);
            return View("index", productList);
        }
        public IActionResult SearchForm() 
        {
            return View();
            //form for insert like edit
        }
        public IActionResult ProcessCreate() 
        {
            Db_ProductDao products = new Db_ProductDao();
            //insert
            return View("index", products.GetAllProducts());
        }
        public IActionResult delete(int id) 
        {
            Db_ProductDao products = new Db_ProductDao();
            product_model product = products.GetProductById(id);
            products.Delete(product);
            return View("index", products.GetAllProducts());
        }
    }
}