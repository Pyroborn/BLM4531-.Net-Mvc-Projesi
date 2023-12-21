using Microsoft.AspNetCore.Authentication.Cookies;
using Mysitemvc.Models;
using System.Security.Claims;
using System.Security.Policy;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;


namespace Mysitemvc.Services
{
    public class SecurityService
    {
        UsersDAO usersDAO = new UsersDAO();
        public SecurityService()
        { 
            
        }

        //connection to usersdao function
        public bool IsValid(Usermodel user)
        {
            if (user == null)
            {
                return false;
            }
            if (user.Locked) 
            {
                return false;
            }
            List<Claim> claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.NameIdentifier, user.Id.ToString( )));
            claims.Add(new Claim("UserName", user.UserName ?? string.Empty));
            ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            ClaimsPrincipal principal = new ClaimsPrincipal(claimsIdentity);
            HttpContextAccessor.HttpContext?.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
            return usersDAO.FindUserByNameAndPassword(user);
        }

    }
}
