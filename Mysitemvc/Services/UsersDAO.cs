using static System.Net.Mime.MediaTypeNames;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Drawing;
using System;
using Mysitemvc.Models;
using System.Data.SqlClient;
using System.Data;
using Microsoft.AspNetCore.Mvc;
using Mysitemvc.Services;

namespace Mysitemvc.Services
{
    public class UsersDAO
    {
        string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Test;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        private readonly TokenValidationService _tokenValidationService;

        public UsersDAO(TokenValidationService tokenValidationService)
        {
            _tokenValidationService = tokenValidationService;
        }

        public UsersDAO() // had to make this one
        {
        }

        


        private readonly UsersDAO _usersDAO;

        public bool IsValid(Usermodel user)
        {
           var existingUser = GetUserbyId(user.Id);

            if (existingUser != null )
            {
                var authenticatedUser = AuthenticateUser(user.UserName, user.Password);
                return authenticatedUser != null; 
            }

            if (user == null || user.Locked)
            {
                return false;
            }

            return UsernameExists(user.UserName);
        }

        public Usermodel AuthenticateUser(string UserName, string Password)
        {
            Usermodel user = null;
            string sqlStatement = "SELECT * FROM dbo.Users WHERE username = @Username  AND password = @Password";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(sqlStatement, connection);
                command.Parameters.Add("@Username", System.Data.SqlDbType.VarChar, 40).Value = UserName;
                command.Parameters.Add("@Password", System.Data.SqlDbType.VarChar, 40).Value = Password;

                //try catch to avoid crashes hopefully
                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    if(reader.HasRows)
                    {
                        user = new Usermodel();
                        user.UserName = reader["UserName"].ToString();
                        user.Password = reader["Password"].ToString();
                        string? vr = reader["Role"].ToString();
                        user.Roles = vr.Split(',').ToList();
                    }
                }catch(Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            return user;
        }

        public int Insert(Usermodel user)
        {
            int InsertedId = 0;
            if (UsernameExists(user.UserName))
            {
                Console.WriteLine("User with the same username already exists.");
                return 0; 
            }
            string sqlStatement = "INSERT INTO dbo.Users (UserName, Password) OUTPUT INSERTED.ID VALUES (@UserName, @Password)";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand sqlCommand = new SqlCommand(sqlStatement, connection))
                {
                    sqlCommand.Parameters.AddWithValue("@UserName", user.UserName);
                    sqlCommand.Parameters.AddWithValue("@Password", user.Password);
                    try
                    {
                        connection.Open();
                        InsertedId = (int)sqlCommand.ExecuteScalar();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }

                return InsertedId;
            }

        }

        public bool UsernameExists(string username)
        {
            string check = "SELECT COUNT(*) FROM dbo.Users WHERE UserName = @UserName";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand sqlCommand = new SqlCommand(check, connection))
                {
                    sqlCommand.Parameters.AddWithValue("@UserName", username);

                    try
                    {
                        connection.Open();
                        int count = (int)sqlCommand.ExecuteScalar();
                        return count > 0;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                        return false;
                    }
                }
            }
        }

        public List<Usermodel> GetAllUsers()
        {
            List<Usermodel> userList = new List<Usermodel>();
            string sqlStatement = "SELECT * FROM dbo.Users";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand sqlCommand = new SqlCommand(sqlStatement, connection))
                {
                    try
                    {
                        connection.Open();
                        using (SqlDataReader reader = sqlCommand.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Usermodel user = new Usermodel();
                                user.Id = reader.GetInt32(0);
                                user.UserName = reader.GetString(1);
                                user.Password = reader.GetString(2);
                                userList.Add(user);
                            }
                        }
                    }catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
                return userList;
            }
        }

        public Usermodel GetUserbyId(int id)
        {
            Usermodel found_user = null;
            string sqlStatement = "SELECT * FROM dbo.Users WHERE Id LIKE @Id";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand sqlCommand = new SqlCommand(sqlStatement, connection))
                {
                    sqlCommand.Parameters.AddWithValue("@Id", id);
                    try
                    {
                        connection.Open();
                        using (SqlDataReader reader = sqlCommand.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Usermodel user = new Usermodel();
                                user.Id = reader.GetInt32(0);
                                user.UserName = reader.GetString(1);
                                user.Password = reader.GetString(2);
                                found_user = user;
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }

                    return found_user;
                }
            }
        }

        public int Delete(Usermodel user)
        {
            if (user == null)
            {
                Console.WriteLine("Error: Product object is null.");
                return 0; 
            }
            int rowsAffected = 0;
            string sqlStatement = "DELETE FROM dbo.Users WHERE Id = @Id";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand sqlCommand = new SqlCommand(sqlStatement, connection))
                {
                    sqlCommand.Parameters.AddWithValue("@Id", user.Id);
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

        public int Update(Usermodel user)
        {
            int rowsaffected = 0;
            string sqlStatement = "UPDATE dbo.Users SET UserName = @UserName, Password = @Password WHERE Id = @Id";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand sqlCommand = new SqlCommand(sqlStatement, connection))
                {
                    sqlCommand.Parameters.Add("@UserName", SqlDbType.NVarChar).Value = user.UserName;
                    sqlCommand.Parameters.Add("@Password", SqlDbType.NVarChar).Value = user.Password;
                    sqlCommand.Parameters.Add("@Id", SqlDbType.Int).Value = user.Id;
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
