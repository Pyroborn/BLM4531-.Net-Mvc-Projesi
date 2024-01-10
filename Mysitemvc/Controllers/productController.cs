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
        public IActionResult ShowDetails(int id) 
        {
            Db_ProductDao product = new Db_ProductDao();
            product_model foundProduct = product.GetProductById(id);
            if (foundProduct == null)
            {
                return NotFound(); // or handle the case where the product is not found
            }

            return View(foundProduct);
        }
        public IActionResult ShowOneProductJSON(int id) 
        { 
            Db_ProductDao product = new Db_ProductDao();
            return Json(product.GetProductById(id));
        }
        public IActionResult Edit(int id)
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
        public IActionResult CreatePage () 
        {
            return View("CreateForm");
        }
        public IActionResult Create(product_model product) 
        {
            Db_ProductDao products = new Db_ProductDao();
            products.Insert(product);
            return View("index", products.GetAllProducts());
        }
        public IActionResult Delete(int id) 
        {
            Db_ProductDao products = new Db_ProductDao();
            product_model product = products.GetProductById(id);
            products.Delete(product);
            return View("index", products.GetAllProducts());
        }
    }
}