using Mysitemvc.Models;
namespace Mysitemvc.Services
{
    public interface IUserDataService
    {
        List<Usermodel> GetAllUsers();
        List<Usermodel> SearchUser(string searchTerm);
        Usermodel GetUserById(int id);
        int Insert(Usermodel user);
        int Delete(Usermodel user);
        int Update(Usermodel user);
    }
}
