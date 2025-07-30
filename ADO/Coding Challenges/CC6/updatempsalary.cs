using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CC6
{
    class updatempsalary
    {
        static void Main()
        {
            string connectionString = "Data Source=anahbbrdba-102\\SATHVIKA;Initial Catalog=Coding_Challenge;Integrated Security=True;TrustServerCertificate=True";

            Console.Write("Enter Employee ID to update salary: ");
            int empid = Convert.ToInt32(Console.ReadLine());

            using (SqlConnection con = new SqlConnection(connectionString))
             {
                con.Open();
                using (SqlCommand cmd = new SqlCommand("sp_updateempSalary", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                 cmd.Parameters.AddWithValue("@EmpId", empid);

                    SqlParameter outputParam = new SqlParameter("@updatedsalary", SqlDbType.Decimal)
                    {
                        Direction = ParameterDirection.Output,
                        Precision = 10,
                        Scale = 2
                    };
                    cmd.Parameters.Add(outputParam);

                    cmd.ExecuteNonQuery();
                     decimal updatedSalary = Convert.ToDecimal(outputParam.Value);
                    Console.WriteLine($" Salary updated successfully: {updatedSalary:C}");
                }
                using (SqlCommand cmd = new SqlCommand("SELECT EmpId, Name, Salary, Gender, NetSalary FROM Employee_Details WHERE EmpId = @EmpId", con))
                {
                    cmd.Parameters.AddWithValue("@EmpId", empid);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            Console.WriteLine("************************ Updated Employee Details:***************************************************");
                            Console.WriteLine($"EmpId: {reader["EmpId"]}");
                             Console.WriteLine($"Name: {reader["Name"]}");
                            Console.WriteLine($"Salary: {reader["Salary"]}");
                           Console.WriteLine($"Gender: {reader["Gender"]}");
                            Console.WriteLine($"NetSalary: {reader["NetSalary"]}");
                        }

                    }
                }
            }
        }

    }
}
