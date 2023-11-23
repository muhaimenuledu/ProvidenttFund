// using System.Data.SqlClient;
// using System.Diagnostics;
// using Microsoft.AspNetCore.Mvc;
// using Prototype1.Models;
using MySql.Data.MySqlClient;
using UserProfile.Models;

namespace UserProfile.Models
{
    public class SalaryModel
    {
        public string ID { get; set; }
        public string Amount { get; set; }

        public int SaveDetails()
        {
            string connStr = "server=localhost;user=magento;database=dotnet;port=3306;password=magento";
            MySql.Data.MySqlClient.MySqlCommand cmd;
            MySqlConnection conn = new MySqlConnection(connStr);
            cmd = new MySql.Data.MySqlClient.MySqlCommand();
            conn.Open();
            cmd.Connection = conn;
            cmd.CommandText = "INSERT INTO salary VALUES('" + ID + "','" + Amount + "')";
            cmd.Prepare();
            int i = cmd.ExecuteNonQuery();
            conn.Close();
            return i;
        }
    }
}