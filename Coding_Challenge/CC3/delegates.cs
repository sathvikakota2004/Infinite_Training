using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CC3
{
    public delegate int CalculatorDelegate(int x, int y);

    class Calculator
    {

        public static int Add(int x, int y) => x + y;
        public static int Subtract(int x, int y) => x - y;
        public static int Multiply(int x, int y) => x * y;

        static void Main(string[] args)
        {

            Console.Write("Enter x: ");
            int no1 = Convert.ToInt32(Console.ReadLine());
            Console.Write("Enter y: ");
            int no2 = Convert.ToInt32(Console.ReadLine());
            int addResult = Add(no1, no2);
            Console.WriteLine($"Addition of {no1} and {no2} is: {addResult}");
            int subResult = Subtract(no1, no2);
            Console.WriteLine($"Subtraction of {no1} and {no2} is: {subResult}");
            int mulResult = Multiply(no1, no2);
            Console.WriteLine($"Multiplication of {no1} and {no2} is: {mulResult}");

            Console.Read();








        }
    }
}
