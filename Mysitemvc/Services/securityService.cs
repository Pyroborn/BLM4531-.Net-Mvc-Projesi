using Mysitemvc.Models;
using System.Security.Policy;

namespace Mysitemvc.Services
{
    public class securityService
    {
        UsersDAO usersDAO = new UsersDAO();
        public securityService()
        { 
            
        }

        //connection to usersdao function
        public bool IsValid(Usermodel user)
        {
            return usersDAO.FindUserByNameAndPassword(user);
        }
    }
}
