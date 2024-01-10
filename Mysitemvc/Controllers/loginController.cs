using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Mysitemvc.Entities;
using Mysitemvc.Models;
using Mysitemvc.Services;
using System.Security.Claims;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
namespace Mysitemvc.Controllers

{

    public class loginController : Controller
    {
        private readonly UsersDAO _usersDAO;
        public loginController()
        {
            _usersDAO = new UsersDAO(); // Initialize your UsersDAO
        }


        public IActionResult Index()
        {
            return View();
        }
        public IActionResult ProcessLogin(Usermodel user)
        {
            if (_usersDAO.IsValid(user))
            {
                return RedirectToAction("Index", "Users");
            }
            else
            {
                ModelState.AddModelError("", "Invalid credentials");
                return View("Index");
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
        

        

    }
}
