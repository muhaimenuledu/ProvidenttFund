// using System.Data.SqlClient;
// using System.Diagnostics;
// using Microsoft.AspNetCore.Mvc;
// using Prototype1.Models;
using MySql.Data.MySqlClient;
using UserProfile.Models;

namespace UserProfile.Models
{
    public class ProfitModel
    {
        public double Invest { get; set; }
        public double Profit { get; set; }
        public string PeriodFrom { get; set; }
        public string PeriodTo { get; set; }
        public string LastMonth { get; set; }

        public double total_fund;
        public double actual_invest;

        public double rate;
        public double share;

        public string  emp_id;

        public int SaveDetails()
        {
            string connStr = "server=localhost;user=magento;database=dotnet;port=3306;password=magento";
            MySql.Data.MySqlClient.MySqlCommand cmd;
            MySqlConnection conn = new MySqlConnection(connStr);
            cmd = new MySql.Data.MySqlClient.MySqlCommand();
            conn.Open();
            cmd.Connection = conn;
            cmd.CommandText = "SELECT * FROM provident WHERE period_from= '" + LastMonth + "' ";
            cmd.Prepare();
            var reader = cmd.ExecuteReader();
            while (reader.Read())
            {


                double employee_contribution = Convert.ToDouble(reader["closing_blnc_emp"]);

                total_fund = total_fund + employee_contribution;



                // double rate = (employee_contribution / Invest) * 100;

                // actual_invest = (employee_contribution * Invest) / total_fund;

                // Console.WriteLine(reader["employee_id"]);
                // Console.WriteLine(employee_contribution);
                // Console.WriteLine(rate);

                // Console.WriteLine("Actual Invest: " + total_fund);
            }
            Console.WriteLine("Total Fund: " + total_fund);
            profit(total_fund);


            // int i = cmd.ExecuteNonQuery();
            conn.Close();
            // Console.WriteLine(i);
            int i = 00;
            return i;
        }

        public int profit(double total_fund)
        {
            string connStr = "server=localhost;user=magento;database=dotnet;port=3306;password=magento";
            MySql.Data.MySqlClient.MySqlCommand cmd;
            MySqlConnection conn = new MySqlConnection(connStr);
            cmd = new MySql.Data.MySqlClient.MySqlCommand();
            conn.Open();
            cmd.Connection = conn;
            cmd.CommandText = "SELECT * FROM provident WHERE period_from= '" + LastMonth + "' ";
            cmd.Prepare();
            var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                double employee_contribution = Convert.ToDouble(reader["closing_blnc_emp"]);
                actual_invest = (employee_contribution * Invest) / total_fund;
                Console.WriteLine(reader["employee_id"]);
                Console.WriteLine("Employee Balance on the month of " + LastMonth + " is tk " + reader["closing_blnc_emp"]);
                Console.WriteLine("Actual Invest: " + actual_invest);

                rate = (actual_invest*100)/Invest;
                Console.WriteLine("Rate "+ rate);
                share = (rate*Profit)/100;
                Console.WriteLine("Share OF Profit on the month of " + PeriodFrom + " is tk " + share);

                emp_id = Convert.ToString (reader["employee_id"]);
                
                testprint();
                saveProfit(emp_id, share);
                

            }
            int o = 0;
            return o;
        }

        public int saveProfit(string emp_id, double profit)
        {

            string connStr = "server=localhost;user=magento;database=dotnet;port=3306;password=magento";
            MySql.Data.MySqlClient.MySqlCommand cmd;
            MySqlConnection conn = new MySqlConnection(connStr);
            cmd = new MySql.Data.MySqlClient.MySqlCommand();
            conn.Open();
            cmd.Connection = conn;
            cmd.CommandText = "INSERT INTO profit VALUES('" + emp_id + "','" + PeriodFrom + "','" + PeriodTo + "','" + profit + "')";
            cmd.Prepare();
            int sp = cmd.ExecuteNonQuery();
            Console.WriteLine("testing" + sp);
            return sp;
        }


                public int  testprint ()
        {
            Console.WriteLine ("////////////////");
            int x = 0;
            return x;
        }
    }
}