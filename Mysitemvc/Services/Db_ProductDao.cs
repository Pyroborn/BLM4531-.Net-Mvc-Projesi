using Mysitemvc.Models;
using System.Collections.Immutable;
using System.Data.SqlClient;

namespace Mysitemvc.Services
{
    public class Db_ProductDao : IProductDataService
    {
        private string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Test;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        public int Delete(product_model product)
        {
            if (product == null)
            {
                Console.WriteLine("Error: Product object is null.");
                return 0; // or handle the situation accordingly
            }
            int rowsAffected = 0;
            string sqlStatement = "DELETE FROM dbo.Products WHERE Id = @Id";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand sqlCommand = new SqlCommand(sqlStatement, connection))
                {
                    sqlCommand.Parameters.AddWithValue("@Id", product.Id);
                    try
                    {
                        connection.Open();
                        rowsAffected = sqlCommand.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }

                return rowsAffected;
            }
        }

        public List<product_model> GetAllProducts()
        {
            List<product_model> productsList = new List<product_model>();
            string sqlStatement = "SELECT * FROM dbo.Products";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand sqlCommand = new SqlCommand(sqlStatement, connection))
                {
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
        }

        public product_model GetProductById(int id)
        {
            product_model found_product = null;
            string sqlStatement = "SELECT * FROM dbo.Products WHERE Id LIKE @Id";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand sqlCommand = new SqlCommand(sqlStatement, connection))
                {
                    sqlCommand.Parameters.AddWithValue("@Id", id);
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
                            found_product = product;
                        }

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }

                    return found_product;
                }
            }
        }

        public int Insert(product_model product)
        {
            int rowsAffected = 0;
            string sqlStatement = "INSERT INTO dbo.Products (Name, Price, Description) VALUES (@Name, @Price, @Description)";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand sqlCommand = new SqlCommand(sqlStatement, connection))
                {
                    sqlCommand.Parameters.AddWithValue("@Name", product.Name);
                    sqlCommand.Parameters.AddWithValue("@Price", product.Price);
                    sqlCommand.Parameters.AddWithValue("@Description", product.Description);
                    try
                    {
                        connection.Open();
                        rowsAffected = sqlCommand.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
        
            return rowsAffected;
        }
   
        }

        public List<product_model> SearchProducts(string searchTerm)
        {
            List<product_model> productsList = new List<product_model>();
            string sqlStatement = "SELECT * FROM dbo.Products WHERE Name LIKE @Name";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand sqlCommand = new SqlCommand(sqlStatement, connection))
                {
                    sqlCommand.Parameters.AddWithValue("@Name", '%' + searchTerm + '%');
                    try
                    {
                        connection.Open();
                        using (SqlDataReader reader = sqlCommand.ExecuteReader())
                        {
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

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }

                    return productsList;
                }
            }
        }

        public int Update(product_model product)
        {
            int rowsaffected = 0;
            string sqlStatement = "UPDATE dbo.Products SET Name = @Name, Price = @Price, Description = @Description WHERE Id = @Id";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand sqlCommand = new SqlCommand(sqlStatement, connection))
                {
                    sqlCommand.Parameters.AddWithValue("@Id", product.Id);
                    sqlCommand.Parameters.AddWithValue("@Name", product.Name);
                    sqlCommand.Parameters.AddWithValue("@Price", product.Price);
                    sqlCommand.Parameters.AddWithValue("@Description", product.Description);
                    try
                    {
                        connection.Open();
                        rowsaffected = sqlCommand.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    return rowsaffected;
                }
            }
        }
    }


}
    

