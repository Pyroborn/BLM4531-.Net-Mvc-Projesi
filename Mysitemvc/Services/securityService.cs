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
    
        public SecurityService()
        { 
            
        }

    
        public ClaimsPrincipal? GetClaimsPrincipal(Usermodel user)
        {
            if (user == null)
            {
             
                return null;
            }

            if (user.Locked)
            {
                
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
