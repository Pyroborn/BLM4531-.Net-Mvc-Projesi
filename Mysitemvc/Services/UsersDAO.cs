using static System.Net.Mime.MediaTypeNames;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Drawing;
using System;
using Mysitemvc.Models;
using System.Data.SqlClient;

namespace Mysitemvc.Services
{
    public class UsersDAO
    {
        string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Test;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        //
        public bool FindUserByNameAndPassword(Usermodel user)
        {
            bool success = false;
            string sqlStatement = "SELECT * FROM dbo.Users WHERE username = @Username  AND password = @Password";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(sqlStatement, connection);
                command.Parameters.Add("@Username", System.Data.SqlDbType.VarChar, 40).Value = user.UserName;
                command.Parameters.Add("@Password", System.Data.SqlDbType.VarChar, 40).Value = user.Password;

                //try catch to avoid crashes hopefully
                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    if(reader.HasRows)
                    {
                        success = true;
                    }
                }catch(Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }

                return success;
        }
    }
}
