using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CC6
{
    

    class Program
    {
        static void Main(string [] args)
        {
            
            Console.WriteLine("Enter Employee Name: ");
            string name = Console.ReadLine();

           
            Console.Write("Enter Salary: ");
            decimal salary = Convert.ToDecimal(Console.ReadLine());
           


            Console.Write("Enter Gender : ");
            string gender = Console.ReadLine();

            
            string connectionString = "Data Source=anahbbrdba-102\\SATHVIKA;Initial Catalog=Coding_Challenge;Integrated Security=True;TrustServerCertificate=True";

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("sp_empinsert", con);
                cmd.CommandType = CommandType.StoredProcedure;

               
                cmd.Parameters.AddWithValue("@Name", name);
                 cmd.Parameters.AddWithValue("@Salary", salary);
                cmd.Parameters.AddWithValue("@Gender", gender);
                cmd.Parameters.AddWithValue("@Empidnew", DBNull.Value).Direction = ParameterDirection.Output;

                SqlParameter netSalaryParam = new SqlParameter("@netsalarynew", SqlDbType.Decimal)
                {Precision = 10,
                    Scale = 2,
                    Direction = ParameterDirection.Output};
                cmd.Parameters.Add(netSalaryParam); con.Open();
                cmd.ExecuteNonQuery();
                int empId = (int)cmd.Parameters["@Empidnew"].Value;
                decimal netSalary = (decimal)cmd.Parameters["@netsalarynew"].Value;
                Console.WriteLine($"***********************************Employee record inserted successfully******************************************");
               Console.WriteLine($"EmpId: {empId}");
                Console.WriteLine($"Employee Name :{ name}");
                 Console.WriteLine($"Employee Salary:{salary}");
                Console.WriteLine($"Employee Gender:{gender}");
                Console.WriteLine($"Net Salary:{netSalary}");
            }
        }
    }

}
