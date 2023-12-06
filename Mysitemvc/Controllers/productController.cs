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
    }
}