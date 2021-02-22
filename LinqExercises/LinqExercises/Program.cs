using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace LinqExercises
{
    class Program
    {
        static void Main(string[] args)
        {
            var query = PersonsDatabase.AllPersons()
                .Select((p, index) => new { Name = p.FullName, Index = index });

            //IEnumerable<string> query = from p in PersonsDatabase.AllPersons()
            //                            select p.FullName;

            foreach (var personNameAndIndex in query)
            {
                Console.WriteLine($"{personNameAndIndex.Index}) {personNameAndIndex.Name}");
            }
        }

        private static void MyFirstLinqQuery()
        {
            Console.Write("N=");
            string nValueAsString = Console.ReadLine();
            if (!int.TryParse(nValueAsString, out int n))
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

                if (count >= n)
                {
                    break;
                }
            }
        }

        private static void Where_Basic()
        {
            foreach (Person p in PersonsDatabase.AllPersons())
            {
                p.Print();
            }

            Console.WriteLine("-----------------------------------------------------");
            Console.WriteLine("Persons over 14 years with full name starting with 'M'");
            Console.WriteLine("-----------------------------------------------------");

            var query = PersonsDatabase.AllPersons()
                .Where(p => (p.Age >= 14) &&
                            p.FullName.StartsWith("M"));

            // Or:
            //var query = from p in PersonsDatabase.AllPersons()
            //            where (p.Age >= 14) && p.FullName.StartsWith("M")
            //            select p;

            foreach (Person p in query)
            {
                p.Print();
            }
        }

        private static void Where_With_Index()
        {
            int index = 0;
            foreach (Person p in PersonsDatabase.AllPersons())
            {
                p.Print(index);
                index++;
            }

            Console.WriteLine("-----------------------------------------------------");
            Console.WriteLine("Persons over 14 years with full name starting with 'M'");
            Console.WriteLine("-----------------------------------------------------");

            var query1 = PersonsDatabase.AllPersons()
                .Where(p => (p.Age >= 14) &&
                                     p.FullName.StartsWith("M"));

            //var query1 = from p in PersonsDatabase.AllPersons()
            //            where (p.Age >= 14) && p.FullName.StartsWith("M")
            //            select p;

            index = 0;
            foreach (Person p in query1)
            {
                p.Print(index);
                index++;
            }

            Console.WriteLine("-----------------------------------------------------");
            Console.WriteLine("Persons over 14 years with full name starting with 'M' and on even index");
            Console.WriteLine("-----------------------------------------------------");

            var query2 = query1.Where((p, index) => index % 2 == 0);

            foreach (Person p in query2)
            {
                p.Print();
            }
        }
    }
}
