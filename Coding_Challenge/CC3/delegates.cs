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
            int num1 = Convert.ToInt32(Console.ReadLine());
            Console.Write("Enter y: ");
            int num2 = Convert.ToInt32(Console.ReadLine());

            int addResult = Add(num1, num2);
            Console.WriteLine($"Addition of {num1} and {num2} is: {addResult}");
            int subResult = Subtract(num1, num2);
            Console.WriteLine($"Subtraction of {num1} and {num2} is: {subResult}");
            int mulResult = Multiply(num1, num2);
            Console.WriteLine($"Multiplication of {num1} and {num2} is: {mulResult}");

            Console.Read();








        }
    }
}
