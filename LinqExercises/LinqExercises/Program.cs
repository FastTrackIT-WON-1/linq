using System;
using System.Collections.Generic;
using System.Linq;

namespace LinqExercises
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("N=");
            string nValueAsString = Console.ReadLine();
            if(!int.TryParse(nValueAsString, out int n))
            {
                throw new ArgumentException($"'{n}' must be a numeric value");
            }

            // IEnumerable<int> query = NumbersGenerator.Next().Where(nr => nr % 2 == 0);
            IEnumerable<int> query = from nr in NumbersGenerator.Next()
                        where nr % 2 == 0
                        select nr;

            int count = 0;
            foreach (int element in query)
            {
                Console.WriteLine(element);
                count++;

                if(count >= n)
                {
                    break;
                }
            }
        }
    }
}
