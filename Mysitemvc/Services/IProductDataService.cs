using Mysitemvc.Models;
//interface for basic operations
namespace Mysitemvc.Services
{
    public interface IProductDataService
    {
        List<product_model> GetAllProducts();
        List<product_model> SearchProducts(string searchTerm);
        product_model GetProductById(int id);
        int Insert(product_model product);
        int Delete(product_model product);
        int Update(product_model product);
    }
}
