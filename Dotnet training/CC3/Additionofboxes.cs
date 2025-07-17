using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CC3
{
    class Box
    {
        public int Length { get; set; }
        public int Breadth { get; set; }

        public static Box Add(Box b1, Box b2)
        {
            Box result = new Box();
            result.Length = b1.Length + b2.Length;
            result.Breadth = b1.Breadth + b2.Breadth;
            return result;
        }

        public void Display()
        {
            Console.WriteLine($"Result = BOX 3 Length: {Length}, BOX 3Breadth: {Breadth}");
        }
    }

    class TestBox
    {
        static void Main(string[] args)
        {

            Console.Write("BOX 1 Length: ");
            int length1 = Convert.ToInt32(Console.ReadLine());
            Console.Write("BOX 1 Breadth: ");
            int breadth1 = Convert.ToInt32(Console.ReadLine());

            Box box1 = new Box();
            box1.Length = length1;
            box1.Breadth = breadth1;


            Console.Write("BOX 2 Length: ");
            int length2 = Convert.ToInt32(Console.ReadLine());
            Console.Write("BOX 2 Breadth: ");
            int breadth2 = Convert.ToInt32(Console.ReadLine());

            Box box2 = new Box();
            box2.Length = length2;
            box2.Breadth = breadth2;


            Box box3 = Box.Add(box1, box2);



            box3.Display();


            Console.Read();
        }
    }
}
