// using System.Data.SqlClient;
// using System.Diagnostics;
// using Microsoft.AspNetCore.Mvc;
// using Prototype1.Models;
using MySql.Data.MySqlClient;
using UserProfile.Models;

namespace UserProfile.Models
{
    public class LoginModel
    {
        public string Name { get; set; }
        public string Password { get; set; }
        public string Type { get; set; }
        // public string Email { get; set; }
        public int SaveDetails()
        {
            int j = 99;
            string connStr = "server=localhost;user=magento;database=dotnet;port=3306;password=magento";
            MySql.Data.MySqlClient.MySqlCommand cmd;
            MySqlConnection conn = new MySqlConnection(connStr);
            cmd = new MySql.Data.MySqlClient.MySqlCommand();
            conn.Open();
            cmd.Connection = conn;
            // cmd.CommandText = "INSERT INTO user VALUES('" + Name + "','" + Password + "','" + Type + "')";
            // cmd.CommandText = "select * from demo";
            cmd.CommandText = "select * from user where username='" + Name + "' and password='" + Password + "' ";
            cmd.Prepare();
            int i = cmd.ExecuteNonQuery();
            Console.WriteLine("sql response" + i);
            if (i > 0)
            {
                return i;
            }
            else
            {
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Console.WriteLine(reader["username"]);
                        Console.WriteLine(reader["password"]);
                        string type = reader["user_type"].ToString();
                        if (type == "admin")
                        {
                            j = 4;
                        }
                        else if (type == "user")
                        {
                            j = 5;
                        }
                    }
                }
            }
            conn.Close();
            return j;
        }
    }
}