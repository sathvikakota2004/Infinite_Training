using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using DataAccess;


namespace ElectricityWeb.App_Code
{
    public class ElectricityBoard
    {
        private readonly DBHandler db = new DBHandler();

        public void CalculateBill(ElectricityBill ebill)
        {
            int u = ebill.UnitsConsumed;
            double total = 0;

            if (u <= 100)
            {
                total = 0;
            }
            else if (u <= 300)
            {
                total = (u - 100) * 1.5;
            }
            else if (u <= 600)
            {
                total = (200 * 1.5) + (u - 300) * 3.5;
            }
            else if (u <= 1000)
            {
                total = (200 * 1.5) + (300 * 3.5) + (u - 600) * 5.5;
            }
            else
            {
                total = (200 * 1.5) + (300 * 3.5) + (400 * 5.5) + (u - 1000) * 7.5;
            }

            ebill.BillAmount = total;
        }

        public void AddBill(ElectricityBill ebill)
        {
            using (SqlConnection con = db.GetConnection())
            using (SqlCommand cmd = con.CreateCommand())
            {
                con.Open();
                cmd.CommandText = @"INSERT INTO ElectricityBill
                (consumer_number, consumer_name, units_consumed, bill_amount)
                VALUES (@no, @name, @units, @amount)";
                cmd.Parameters.AddWithValue("@no", ebill.ConsumerNumber);
                cmd.Parameters.AddWithValue("@name", ebill.ConsumerName);
                cmd.Parameters.AddWithValue("@units", ebill.UnitsConsumed);
                cmd.Parameters.AddWithValue("@amount", ebill.BillAmount);
                cmd.ExecuteNonQuery();
            }
        }

        // Returns the last N by consumer_number desc (no ID/timestamp available per spec)
        public List<ElectricityBill> Generate_N_BillDetails(int n)
        {
            var list = new List<ElectricityBill>();
            using (SqlConnection con = db.GetConnection())
            using (SqlCommand cmd = con.CreateCommand())
            {
                con.Open();
                cmd.CommandText = @"SELECT TOP (@n)
                consumer_number, consumer_name, units_consumed, bill_amount
                FROM ElectricityBill
                ORDER BY consumer_number DESC";
                cmd.Parameters.AddWithValue("@n", n);

                using (var r = cmd.ExecuteReader())
                {
                    while (r.Read())
                    {
                        var eb = new ElectricityBill
                        {
                            ConsumerNumber = r["consumer_number"].ToString(),
                            ConsumerName = r["consumer_name"].ToString(),
                            UnitsConsumed = Convert.ToInt32(r["units_consumed"]),
                            BillAmount = Convert.ToDouble(r["bill_amount"])
                        };
                        list.Add(eb);
                    }
                }
            }
            return list;
        }
    }
}