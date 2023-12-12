using Mysitemvc.Models;
using System.Collections.Immutable;
using System.Data.SqlClient;

namespace Mysitemvc.Services
{
    public class Db_ProductDao : IProductDataService
    {

        public int Delete(product_model product)
        {
            int newIdNumber = -1;
            string sqlStatement = "DELETE FROM dbo.Products WHERE Id LIKE @Id";
            using (SqlConnection connection = new SqlConnection())
            {
                SqlCommand sqlCommand = new SqlCommand(sqlStatement, connection);
                try
                {
                    connection.Open();
                    newIdNumber = Convert.ToInt32(sqlCommand.ExecuteScalar());
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

                return newIdNumber;
            }
        }

        public List<product_model> GetAllProducts()
        {
            string connectionString = @"Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Test;Integrated Security=True;Connect Timeout=15;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=True";
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
                        product_model product = new product_model();
                        product.Id = reader.GetInt32(0);
                        product.Name = reader.GetString(1);
                        product.Price = reader.GetDecimal(2);
                        product.Description = reader.GetString(3);
                        productsList.Add(product);
                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            return productsList;
        }

        public product_model GetProductById(int id)
        {
            string connectionString = @"Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Test;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

            product_model found_product = null;
            string sqlStatement = "SELECT * FROM dbo.Products WHERE Id LIKE @Id";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand sqlCommand = new SqlCommand(sqlStatement, connection);
                sqlCommand.Parameters.AddWithValue("@Id", id);
                try
                {
                    connection.Open();
                    SqlDataReader reader = sqlCommand.ExecuteReader();
                    while (reader.Read())
                    {
                        found_product = new product_model(reader.GetInt16(0), reader.GetString(1), reader.GetDecimal(2), reader.GetString(3));
                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

                return found_product;
            }
        }

        public int Insert(product_model product)
        {
            throw new NotImplementedException();
        }

        public List<product_model> SearchProducts(string searchTerm)
        {
            string connectionString = @"Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Test;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

            List<product_model> productsList = new List<product_model>();
            string sqlStatement = "SELECT * FROM dbo.Products WHERE Name LIKE @Name";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand sqlCommand = new SqlCommand(sqlStatement, connection);
                sqlCommand.Parameters.AddWithValue("@Name", '%' + searchTerm + '%');
                try
                {
                    connection.Open();
                    SqlDataReader reader = sqlCommand.ExecuteReader();
                    while (reader.Read())
                    {
                        productsList.Add(new product_model(reader.GetInt16(0), reader.GetString(1), reader.GetDecimal(2), reader.GetString(3)));
                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

                return productsList;
            }
        }
        public int Update(product_model product)
        {
            int newIdNumber = -1;
            string sqlStatement = "UPDATE dbo.Products SET Name = @Name, Price = @Price, Description = @Description WHERE Id LIKE @Id";
            using (SqlConnection connection = new SqlConnection())
            {
                SqlCommand sqlCommand = new SqlCommand(sqlStatement, connection);
                sqlCommand.Parameters.AddWithValue("@Name", product.Name);
                sqlCommand.Parameters.AddWithValue("@Price", product.Price);
                sqlCommand.Parameters.AddWithValue("@Description", product.Description);
                sqlCommand.Parameters.AddWithValue("@Id", product.Id);
                try
                {
                    connection.Open();
                    newIdNumber = Convert.ToInt32(sqlCommand.ExecuteScalar());
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

                return newIdNumber;
            }
        }
    }


}
    

