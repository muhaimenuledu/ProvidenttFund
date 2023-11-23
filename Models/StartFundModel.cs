// using System.Data.SqlClient;
// using System.Diagnostics;
// using Microsoft.AspNetCore.Mvc;
// using Prototype1.Models;
using MySql.Data.MySqlClient;
using UserProfile.Models;

namespace UserProfile.Models
{
    public class StartFundModel
    {
        public string Period_from { get; set; }
        public string Period_to { get; set; }
        public string Employee_ID { get; set; }
        public double Employee_balance_start { get; set; }
        public double Company_balance_start { get; set; }
        public double Employee_amount { get; set; }
        public double Company_amount { get; set; }
        public double Employee_profit { get; set; }
        public double Company_profit { get; set; }
        public double Employee_balance_close { get; set; }
        public double Company_balance_close { get; set; }


        public int SaveDetails()
        {
            string connStr = "server=localhost;user=magento;database=dotnet;port=3306;password=magento";
            MySql.Data.MySqlClient.MySqlCommand cmd;
            MySqlConnection conn = new MySqlConnection(connStr);
            cmd = new MySql.Data.MySqlClient.MySqlCommand();
            conn.Open();
            cmd.Connection = conn;
            // cmd.CommandText = "INSERT INTO provident VALUES('" + Period_from + "','" + Period_to + "','" + Employee_ID + "','" + Employee_balance_start + "''" + Company_balance_start + "','" + Employee_amount + "','" + Company_amount + "','" + Employee_profit + "','" + Company_profit + "','" + Employee_balance_close + "','" + Company_balance_close + "')";
            cmd.CommandText = "INSERT INTO provident (`period_from`, `period_to`, `employee_id`, `opening_blnc_emp`, `opening_blnc_comp`, `emp_amount`, `comp_amount`, `emp_profit`, `comp_profit`, `closing_blnc_emp`, `closing_blnc_comp`) VALUES ('" + Period_from + "','" + Period_to + "','" + Employee_ID + "','" + Employee_balance_start + "','" + Company_balance_start + "','" + Employee_amount + "','" + Company_amount + "','" + Employee_profit + "','" + Company_profit + "','" + Employee_balance_close + "','" + Company_balance_close + "')";
            cmd.Prepare();
            int i = cmd.ExecuteNonQuery();
            conn.Close();
            return i;
        }
    }
}