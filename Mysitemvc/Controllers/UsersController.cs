using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Mysitemvc.Models;
using Mysitemvc.Services;

    namespace Mysitemvc.Controllers
    {
    public class UsersController : Controller
        {
            private readonly UsersDAO _usersDAO;

            public UsersController(UsersDAO usersDAO)
            {
                _usersDAO = usersDAO; // Initialize your UsersDAO using dependency injection
            }


        public IActionResult Index()
        {
            return View();
        }


        [HttpPost]
        public IActionResult Login(Usermodel user)
        {
           
            if (_usersDAO.IsValid(user))
            {
               
                string token = TokenService.GenerateToken(user.UserName);

          
                HttpContext.Response.Cookies.Append("AuthToken", token);
                return RedirectToAction("Dashboard");
               // return Ok(new { Token = token });
            }
            else
            {
                ModelState.AddModelError("", "Invalid credentials");
                return View("Index");
            }
        }

        public IActionResult Dashboard()
        {
            // Action for the authenticated user's dashboard
            return View();
        }




    }
    }
   

