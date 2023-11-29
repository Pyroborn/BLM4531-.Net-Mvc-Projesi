using Microsoft.AspNetCore.Mvc;

namespace Mysitemvc.Controllers
{
    public class productsController : Controller
    {
        public IActionResult Index(string name="Arda")
        {
            ViewBag.Name = name;
            return View();
        }
    }
}