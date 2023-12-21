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
        public IActionResult ProcessLogin(Usermodel usermodel)
        {
            SecurityService securityService = new SecurityService();
            if(securityService.IsValid(usermodel))
            {
                return View("LoginSuccess", usermodel);
            }
            else
            {
                return View("LoginFailure", usermodel);
            }
        }
        public IActionResult register()
        {
            return View("register_form");
        }
        public IActionResult register_process(Usermodel users) 
        {
            UsersDAO user = new UsersDAO();
            user.Insert(users);
            return View("Index");
        }
        public IActionResult show_users()
        {
            UsersDAO users = new UsersDAO();
            List<Usermodel> UserList = users.GetAllUsers();
            return View(UserList);
        }
        public IActionResult Delete(int id)
        {
            UsersDAO users = new UsersDAO();
            Usermodel user = users.GetUserbyId(id);
            users.Delete(user);
            return View("show_users", users.GetAllUsers());
        }
        public IActionResult showDetails(int id)
        {
            UsersDAO user = new UsersDAO();
            Usermodel foundUser = user.GetUserbyId(id);
            return View(foundUser);
        }
        public IActionResult Edit(int id)
        {
            UsersDAO user = new UsersDAO();
            Usermodel foundUser = user.GetUserbyId(id);
            return View("Edit", foundUser);
        }
        public IActionResult ProcessEdit(Usermodel user)
        {
            UsersDAO users = new UsersDAO();
            users.Update(user);
            return View("show_users", users.GetAllUsers());
        }
        public IActionResult CreatePage()
        {
            return View("CreateForm");
        }
        public IActionResult Create(Usermodel user)
        {
            UsersDAO users = new UsersDAO();
            users.Insert(user);
            return View("show_users", users.GetAllUsers());
        }
        public  IActionResult user_cart() 
        {
            Db_ProductDao db_Productdao = new Db_ProductDao();
            return View(db_Productdao.GetAllProducts());
        }
    }
}
