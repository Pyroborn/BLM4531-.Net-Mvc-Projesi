using Mysitemvc.Models;
using System.Collections.Immutable;
using System.Data.SqlClient;

namespace Mysitemvc.Services
{
    public class Db_ProductDao : IProductDataService
    {
        string connectionString = @"Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Test;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        public int Delete(product_model product)
        {
            throw new NotImplementedException();
        }

        public List<product_model> GetAllProducts()
        {
            List<product_model> productsList = new List<product_model>();
            string sqlStatement = "SELECT * FROM dbo.Products";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand sqlCommand = new SqlCommand(sqlStatement, connection);
                try
                {
                    connection.Open();
                    SqlDataReader reader = sqlCommand.ExecuteReader();
                    while (reader.Read())
                    {
                        productsList.Add(new product_model(reader.GetInt16(0), reader.GetString(1), reader.GetDecimal(2), reader.GetString(3)));
                    }

                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            return productsList;
        }

        public product_model GetProductById(int id)
        {
            throw new NotImplementedException();
        }

        public int Insert(product_model product)
        {
            throw new NotImplementedException();
        }

        public List<product_model> SearchProducts(string searchTerm)
        {
            throw new NotImplementedException();
        }

        public int Update(product_model product)
        {
            throw new NotImplementedException();
        }
    }
}
