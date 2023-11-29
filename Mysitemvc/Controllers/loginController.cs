using Microsoft.AspNetCore.Mvc;
using Mysitemvc.Models;
using Mysitemvc.Services;

namespace Mysitemvc.Controllers
{
    public class loginController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        // called by ** then checks if theres a match then views success or failure pages
        public IActionResult ProcessLogin(Usermodel usermodel)
        {
            securityService securityService = new securityService();
            if(securityService.IsValid(usermodel))
            {
                return View("LoginSuccess", usermodel);
            }
            else
            {
                return View("LoginFailure", usermodel);
            }
        }
    }
}
