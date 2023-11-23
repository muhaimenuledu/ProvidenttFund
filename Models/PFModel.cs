// using System.Data.SqlClient;
// using System.Diagnostics;
// using Microsoft.AspNetCore.Mvc;
// using Prototype1.Models;
using MySql.Data.MySqlClient;
using UserProfile.Models;

namespace UserProfile.Models
{
    public class PFModel
    {
        public string PeriodFrom { get; set; }
        public string PeriodTo { get; set; }
        public string LastMonth { get; set; }
        // public double Invest { get; set; }
        // public double Profit { get; set; }

        public double EmpProfit;




        public int checkFund()
        {
            string connStr = "server=localhost;user=magento;database=dotnet;port=3306;password=magento";
            MySql.Data.MySqlClient.MySqlCommand cmd;
            MySqlConnection conn = new MySqlConnection(connStr);
            cmd = new MySql.Data.MySqlClient.MySqlCommand();
            conn.Open();
            cmd.Connection = conn;
            cmd.CommandText = "SELECT * FROM provident WHERE period_from='" + PeriodFrom + "'";

            var reader = cmd.ExecuteReader();

            if (reader.HasRows)
            {
                // update the existing value + the value from the text file
                int l = 1;
                Console.WriteLine("EntryExist" + l);
                return l;
            }
            else
            {
                // insert a value from a text file
                SaveDetails();
                int l = 0;
                // Console.WriteLine("checkFund" + l);
                return l;
            }

        }

        public int SaveDetails()
        {

            string connStr = "server=localhost;user=magento;database=dotnet;port=3306;password=magento";
            MySql.Data.MySqlClient.MySqlCommand cmd;
            MySqlConnection conn = new MySqlConnection(connStr);
            cmd = new MySql.Data.MySqlClient.MySqlCommand();
            conn.Open();
            cmd.Connection = conn;
            cmd.CommandText = "SELECT * FROM demo";
            cmd.Prepare();
            var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                string emp_id = reader["ID"].ToString();
                Console.WriteLine(emp_id);
                getsalary(emp_id);
            }
            int i = 0;
            conn.Close();
            // Console.WriteLine(Invest);
            return i;
        }

        public string getsalary(string emp_id)
        {
            Console.WriteLine("Previous Month" + LastMonth);
            string connStr = "server=localhost;user=magento;database=dotnet;port=3306;password=magento";
            MySql.Data.MySqlClient.MySqlCommand cmd;
            MySqlConnection conn = new MySqlConnection(connStr);
            cmd = new MySql.Data.MySqlClient.MySqlCommand();
            conn.Open();
            cmd.Connection = conn;
            cmd.CommandText = "SELECT * FROM salary WHERE Employee_ID='" + emp_id + "'";
            cmd.Prepare();
            var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                int salary = Convert.ToInt32(reader["Salary"]);
                double basic = salary / 2;
                Console.WriteLine("Salary:" + reader["Salary"]);
                Console.WriteLine("Basic:" + basic);
                getcontribution(emp_id, basic);

            }
            conn.Close();
            string k = "ada";
            return k;
        }

        public double getcontribution(string emp_id, double basic)
        {
            string connStr = "server=localhost;user=magento;database=dotnet;port=3306;password=magento";
            MySql.Data.MySqlClient.MySqlCommand cmd;
            MySqlConnection conn = new MySqlConnection(connStr);
            cmd = new MySql.Data.MySqlClient.MySqlCommand();
            conn.Open();
            cmd.Connection = conn;
            cmd.CommandText = "SELECT * FROM membership WHERE employee_name='" + emp_id + "'";
            cmd.Prepare();
            var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                int rate = Convert.ToInt32(reader["pf_cbtn_rate"]);
                Console.WriteLine("PF COntribution Rate:" + rate + "%");
                double employee_contribution = (basic * rate) / 100;
                Console.WriteLine("Amount per month:" + employee_contribution);

                showProfit(emp_id, employee_contribution);

                // setProvidentFund(emp_id, employee_contribution);
            }
            conn.Close();
            double l = 9.9;
            return l;
        }


        public double showProfit(string emp_id, double employee_contribution)
        {
            string connStr = "server=localhost;user=magento;database=dotnet;port=3306;password=magento";
            MySql.Data.MySqlClient.MySqlCommand cmd;
            MySqlConnection conn = new MySqlConnection(connStr);
            cmd = new MySql.Data.MySqlClient.MySqlCommand();
            conn.Open();
            cmd.Connection = conn;
            cmd.CommandText = "SELECT * FROM profit  WHERE period_from =  '" + PeriodFrom + "' AND employee_id = '" + emp_id + "' ";
            cmd.Prepare();
            var reader = cmd.ExecuteReader();
            if (reader.HasRows){
                while (reader.Read())
                {
                    Console.WriteLine("Profit = "+reader["profit"]);
                    EmpProfit = Convert.ToDouble(reader["profit"]);
                    Console.WriteLine("Profit = "+ EmpProfit);
                }
            }
            else{
                EmpProfit = 0;
                Console.WriteLine("Profit = "+ EmpProfit);
            }


            setProvidentFund(emp_id, employee_contribution, EmpProfit);

            int sp = 0;
            return sp;
        }

        public double setProvidentFund(string emp_id, double employee_contribution, double EmpProfit)
        {
            // string date1 = PeriodFrom;
            // DateTime date = Convert.ToDateTime(date1);
            // date = date.AddMonths(-1);
            // string lastMonth = Convert.ToString(date);
            // Console.WriteLine("Given Input" + lastMonth);
            // Console.WriteLine("In DB" + PeriodFrom);
            // // var onlyDate = date.Date;
            // 
            // string lastMonth = date.ToShortDateString();
            // Console.WriteLine("/" + lastMonth + "/");

            string connStr = "server=localhost;user=magento;database=dotnet;port=3306;password=magento";
            MySql.Data.MySqlClient.MySqlCommand cmd;
            MySqlConnection conn = new MySqlConnection(connStr);
            cmd = new MySql.Data.MySqlClient.MySqlCommand();
            conn.Open();
            cmd.Connection = conn;
            cmd.CommandText = "SELECT * FROM provident WHERE employee_id = '" + emp_id + "' and  period_from = '" + LastMonth + "' ";
            cmd.Prepare();
            int i = cmd.ExecuteNonQuery();
            // Console.WriteLine(i);
            var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                double emp_open_blnc = Convert.ToDouble(reader["opening_blnc_emp"]);
                double comp_open_blnc = Convert.ToDouble(reader["opening_blnc_comp"]);
                double emp_amnt = employee_contribution;
                double comp_amnt = employee_contribution;
                double emp_profit = 0;
                double comp_profit = 0;
                double emp_close_blnc = Convert.ToDouble(reader["closing_blnc_emp"]);
                double comp_close_blnc = Convert.ToDouble(reader["closing_blnc_comp"]);

                emp_open_blnc = emp_close_blnc;
                comp_open_blnc = comp_close_blnc;

                emp_close_blnc = emp_amnt + emp_open_blnc + EmpProfit;
                comp_close_blnc = comp_amnt + comp_open_blnc;

                // Console.WriteLine("---");
                Console.WriteLine("lastMonth :" + (reader["period_from"]) + "~" + emp_open_blnc + "~" + comp_open_blnc + "~" + emp_amnt + "~" + comp_amnt + "~" + emp_profit + "~" + comp_profit
                + "~" + emp_close_blnc + "~" + comp_close_blnc);

                insertFund(PeriodFrom, PeriodTo, emp_id, emp_open_blnc, comp_open_blnc, emp_amnt, comp_amnt, emp_profit, comp_profit, emp_close_blnc, comp_close_blnc);
            }
            double g = 0;
            return g;
        }

        public double insertFund(string period_from, string period_to, string employee_id, double opening_blnc_emp, double opening_blnc_comp, double emp_amnt, double comp_amnt, double emp_profit, double comp_profit, double emp_close_blnc, double comp_close_blnc)
        {

            string connStr = "server=localhost;user=magento;database=dotnet;port=3306;password=magento";
            MySql.Data.MySqlClient.MySqlCommand cmd;
            MySqlConnection conn = new MySqlConnection(connStr);
            cmd = new MySql.Data.MySqlClient.MySqlCommand();
            conn.Open();
            cmd.Connection = conn;
            // cmd.CommandText = "INSERT INTO provident ('" + period_from + "', '"+ period_to +"','" + employee_id + "', '" + opening_blnc_emp + "', '" + opening_blnc_comp + "', '"+ emp_amnt +"', '" + comp_amnt + "', '"+ emp_profit +"', '" + comp_profit + "', '"+ emp_close_blnc +"', '" + comp_close_blnc + "') ";
            cmd.CommandText = "INSERT INTO provident (`period_from`, `period_to`, `employee_id`, `opening_blnc_emp`, `opening_blnc_comp`, `emp_amount`, `comp_amount`, `emp_profit`, `comp_profit`, `closing_blnc_emp`, `closing_blnc_comp`) VALUES ('" + period_from + "','" + period_to + "','" + employee_id + "','" + opening_blnc_emp + "','" + opening_blnc_comp + "','" + emp_amnt + "','" + comp_amnt + "','" + emp_profit + "','" + comp_profit + "','" + emp_close_blnc + "','" + comp_close_blnc + "')";
            cmd.Prepare();
            int i = cmd.ExecuteNonQuery();
            Console.WriteLine(i);

            // showProfit(employee_id);

            double x = 0.0;
            return x;
        }




    }
}