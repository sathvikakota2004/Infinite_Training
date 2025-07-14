using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CC3
{
    internal class Cricketmatch
    {
        public static void Pointscalculation(int no_of_matches)
        {
            int sum = 0;

            for (int i = 0; i < no_of_matches; i++)
            {
                Console.Write($"Enter score of {i + 1}:");
                int score = Convert.ToInt32(Console.ReadLine());
                sum += score;


            }
            double average = (double)sum / no_of_matches;
            Console.WriteLine("Total Matches Played: " + no_of_matches);
            Console.WriteLine("Sum of Scores: " + sum);
            Console.WriteLine("Average Score: " + average);


        }
        static void Main(string[] args)
        {
            Console.Write("Enter the number of matches: ");
            int no_of_matches = Convert.ToInt32(Console.ReadLine());

            Cricketmatch team = new Cricketmatch();
            Cricketmatch.Pointscalculation(no_of_matches);

        }

    }
}
