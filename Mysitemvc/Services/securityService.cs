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
        //UsersDAO usersDAO = new UsersDAO();
        public SecurityService()
        { 
            
        }

        //connection to usersdao function
        public ClaimsPrincipal? GetClaimsPrincipal(Usermodel user)
        {
            if (user == null)
            {
                // Handle the case where the user is null (e.g., log, throw exception)
                return null;
            }

            if (user.Locked)
            {
                // Handle the case where the user is locked (e.g., log, return null)
                return null;
            }

            List<Claim> claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()));

            if (!string.IsNullOrEmpty(user.UserName))
            {
                claims.Add(new Claim(ClaimTypes.Name, user.UserName));

            }

            ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            return new ClaimsPrincipal(claimsIdentity);
        }

       

    }
}
