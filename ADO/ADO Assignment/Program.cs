using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADO_Assignment
{
    public class Program
    {
        static void Main()
        {
            DataTable empTable = new DataTable("Employees");

            empTable.Columns.Add("EmployeeID", typeof(int));
            empTable.Columns.Add("FirstName", typeof(string));
            empTable.Columns.Add("LastName", typeof(string));
            empTable.Columns.Add("Title", typeof(string));
            empTable.Columns.Add("DOB", typeof(DateTime));
            empTable.Columns.Add("DOJ", typeof(DateTime));
            empTable.Columns.Add("City", typeof(string));

            empTable.Rows.Add(1001, "Malcolm", "Daruwalla", "Manager", new DateTime(1984, 11, 16), new DateTime(2011, 6, 8), "Mumbai");
            empTable.Rows.Add(1002, "Asdin", "Dhalla", "AsstManager", new DateTime(1984, 8, 20), new DateTime(2012, 7, 7), "Mumbai");
            empTable.Rows.Add(1003, "Madhavi", "Oza", "Consultant", new DateTime(1987, 11, 14), new DateTime(2015, 4, 12), "Pune");
            empTable.Rows.Add(1004, "Saba", "Shaikh", "SE", new DateTime(1990, 6, 3), new DateTime(2016, 2, 2), "Pune");
            empTable.Rows.Add(1005, "Nazia", "Shaikh", "SE", new DateTime(1991, 3, 8), new DateTime(2016, 2, 2), "Mumbai");
            empTable.Rows.Add(1006, "Amit", "Pathak", "Consultant", new DateTime(1989, 11, 7), new DateTime(2014, 8, 8), "Chennai");
            empTable.Rows.Add(1007, "Vijay", "Natrajan", "Consultant", new DateTime(1989, 12, 2), new DateTime(2015, 6, 1), "Mumbai");
            empTable.Rows.Add(1008, "Rahul", "Dubey", "Associate", new DateTime(1993, 11, 11), new DateTime(2014, 11, 6), "Chennai");
            empTable.Rows.Add(1009, "Suresh", "Mistry", "Associate", new DateTime(1992, 8, 12), new DateTime(2014, 12, 3), "Chennai");
            empTable.Rows.Add(1010, "Sumit", "Shah", "Manager", new DateTime(1991, 4, 12), new DateTime(2016, 1, 2), "Pune");

            
            Console.WriteLine("Employees who joined before 01/01/2015:");
            foreach (DataRow row in empTable.Select("DOJ < '2015-01-01'"))
            {
                Console.WriteLine($"{row["FirstName"]} - DOJ: {((DateTime)row["DOJ"]).ToShortDateString()}");
            }
                

            
            Console.WriteLine(" Employees with DOB after 01/01/1990:");
            foreach (DataRow row in empTable.Select("DOB > '1990-01-01'"))
            {
                Console.WriteLine($" {row["FirstName"]} - DOB: {((DateTime)row["DOB"]).ToShortDateString()}");
            }

            
            Console.WriteLine(" Employees who are Consultant or Associate:");
            foreach (DataRow row in empTable.Select("Title = 'Consultant' OR Title = 'Associate'"))
            {
                Console.WriteLine($"{row["FirstName"]} {row["LastName"]}, Title: {row["Title"]}");
            }

        
            Console.WriteLine($" Total number of employees: {empTable.Rows.Count}");

            
            Console.WriteLine($" Total number of employees in Chennai: {empTable.Select("City = 'Chennai'").Length}");

           
            int maxID = Convert.ToInt32(empTable.Compute("MAX(EmployeeID)", ""));
            Console.WriteLine($" Highest Employee ID: {maxID}");

            
            Console.WriteLine($" Total number of employees who joined after 01/01/2015: {empTable.Select("DOJ > '2015-01-01'").Length}");
            

           
            Console.WriteLine($" Total number of employees who are not Associates: {empTable.Select("Title <> 'Associate'").Length}");

          
            Console.WriteLine(" Employee count by City:");
            var groupByCity = empTable.AsEnumerable().GroupBy(r => r.Field<string>("City"));
            foreach (var group in groupByCity)
            {
                Console.WriteLine($"{group.Key}: {group.Count()}");
            }

            
            Console.WriteLine(" Employee count by City and Title:");
            var groupByCityTitle = empTable.AsEnumerable()
            .GroupBy(r => new { City = r.Field<string>("City"), Title = r.Field<string>("Title") });
            foreach (var group in groupByCityTitle)
            {
                Console.WriteLine($"{group.Key.City}, {group.Key.Title}: {group.Count()}");
            }

            
            Console.WriteLine(" Youngest employee(s):");
            var youngestDob = empTable.AsEnumerable().Max(r => r.Field<DateTime>("DOB"));
            foreach (DataRow row in empTable.Select($"DOB = '#{youngestDob:MM/dd/yyyy}#'"))
            {
                Console.WriteLine($"  {row["FirstName"]} ");
            }
        }
    }
}
