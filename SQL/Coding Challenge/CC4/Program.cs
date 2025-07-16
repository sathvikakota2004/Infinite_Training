using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CC4
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<Employeedetails> empList = new List<Employeedetails>
{
    new Employeedetails { EmployeeID = 1001, FirstName = "Malcolm", LastName = "Daruwalla", Title = "Manager", DOB = DateTime.ParseExact("16-11-1984", "dd-MM-yyyy", null), DOJ = DateTime.ParseExact("08-06-2011", "dd-MM-yyyy", null), City = "Mumbai" },
    new Employeedetails { EmployeeID = 1002, FirstName = "Asdin", LastName = "Dhalla", Title = "AsstManager", DOB = DateTime.ParseExact("20-08-1994", "dd-MM-yyyy", null), DOJ = DateTime.ParseExact("07-07-2012", "dd-MM-yyyy", null), City = "Mumbai" },
    new Employeedetails { EmployeeID = 1003, FirstName = "Madhavi", LastName = "Oza", Title = "Consultant", DOB = DateTime.ParseExact("14-11-1987", "dd-MM-yyyy", null), DOJ = DateTime.ParseExact("12-04-2015", "dd-MM-yyyy", null), City = "Pune" },
    new Employeedetails { EmployeeID = 1004, FirstName = "Saba", LastName = "Shaikh", Title = "SE", DOB = DateTime.ParseExact("03-06-1990", "dd-MM-yyyy", null), DOJ = DateTime.ParseExact("02-02-2016", "dd-MM-yyyy", null), City = "Pune" },
    new Employeedetails { EmployeeID = 1005, FirstName = "Nazia", LastName = "Shaikh", Title = "SE", DOB = DateTime.ParseExact("08-03-1991", "dd-MM-yyyy", null), DOJ = DateTime.ParseExact("02-02-2016", "dd-MM-yyyy", null), City = "Mumbai" },
    new Employeedetails { EmployeeID = 1006, FirstName = "Amit", LastName = "Pathak", Title = "Consultant", DOB = DateTime.ParseExact("07-11-1989", "dd-MM-yyyy", null), DOJ = DateTime.ParseExact("08-08-2014", "dd-MM-yyyy", null), City = "Chennai" },
    new Employeedetails { EmployeeID = 1007, FirstName = "Vijay", LastName = "Natrajan", Title = "Consultant", DOB = DateTime.ParseExact("02-12-1989", "dd-MM-yyyy", null), DOJ = DateTime.ParseExact("01-06-2015", "dd-MM-yyyy", null), City = "Mumbai" },
    new Employeedetails { EmployeeID = 1008, FirstName = "Rahul", LastName = "Dubey", Title = "Associate", DOB = DateTime.ParseExact("11-11-1993", "dd-MM-yyyy", null), DOJ = DateTime.ParseExact("06-11-2014", "dd-MM-yyyy", null), City = "Chennai" },
    new Employeedetails { EmployeeID = 1009, FirstName = "Suresh", LastName = "Mistry", Title = "Associate", DOB = DateTime.ParseExact("12-08-1992", "dd-MM-yyyy", null), DOJ = DateTime.ParseExact("03-12-2014", "dd-MM-yyyy", null), City = "Chennai" },
    new Employeedetails { EmployeeID = 1010, FirstName = "Sumit", LastName = "Shah", Title = "Manager", DOB = DateTime.ParseExact("12-04-1991", "dd-MM-yyyy", null), DOJ = DateTime.ParseExact("02-01-2016", "dd-MM-yyyy", null), City = "Pune" }
};
            Console.WriteLine("All employees details");
       
            foreach (var emp in empList)
            {
                Console.WriteLine($"{emp.EmployeeID} ,{emp.FirstName} ,{emp.LastName}, {emp.Title} ,{emp.DOB:dd-MM-yyyy} ,{emp.DOJ:dd-MM-yyyy} ,{emp.City}");
            }




            Console.WriteLine("Employees whose location is not Mumbai:");
            var notMumbaiEmployees = empList.FindAll(emp => emp.City != "Mumbai");

            foreach (var emp in notMumbaiEmployees)
            {
                Console.WriteLine($"{emp.EmployeeID} {emp.FirstName} {emp.LastName} {emp.Title} {emp.DOB:dd-MM-yyyy} {emp.DOJ:dd-MM-yyyy} {emp.City}");
            }


            Console.WriteLine("Employees who is AsstManager:");

            var asstManagers = empList.Where(emp => emp.Title == "AsstManager");

            foreach (var emp in asstManagers)
            {
                Console.WriteLine($"{emp.EmployeeID} {emp.FirstName} {emp.LastName} {emp.Title} {emp.DOB:dd-MM-yyyy} {emp.DOJ:dd-MM-yyyy} {emp.City}");
            }


            Console.WriteLine("Employees whose Last Name starts with 'S':");

            var lastNameStartsWithS = empList.Where(emp => emp.LastName.StartsWith("S"));

            foreach (var emp in lastNameStartsWithS)
            {
                Console.WriteLine($"{emp.EmployeeID} {emp.FirstName} {emp.LastName} {emp.Title} {emp.DOB:dd-MM-yyyy} {emp.DOJ:dd-MM-yyyy} {emp.City}");
            }




        }
    }
}
