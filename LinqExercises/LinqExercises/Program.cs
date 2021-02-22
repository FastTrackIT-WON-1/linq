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

        private static void Select_Basic_And_With_Index()
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

        private static void SelectMany_Basic()
        {
            int[] collection = { 1, 2, 3 };

            // Result should be:
            // 1, 1, 1, 2, 4, 8, 3, 9, 27 ;

            var query = collection.SelectMany(nr => new int[] { nr, nr * nr, nr * nr * nr });
            //var query = from nr in collection
            //            from powers in new[] { nr, nr * nr, nr * nr * nr }
            //            select powers;

            foreach (int nr in query)
            {
                Console.Write($"{nr}, ");
            }
        }

        private static void SelectMany_Advanced()
        {
            int[] a = { 2, 3, 4, 5 };
            int[] b = { 4, 5, 6, 7, 8 };

            //var query = a
            //    .SelectMany(
            //        elem1 => b, 
            //        (elem1, elem2) => new { Element1 = elem1, Element2 = elem2 })
            //    .Where(
            //        pair => Math.Abs(pair.Element1 - pair.Element2) == 1)
            //    .Select(
            //        pair => $"({pair.Element1}, {pair.Element2})");

            var query = from elem1 in a
                        from elem2 in b
                        where Math.Abs(elem1 - elem2) == 1
                        select $"({elem1}, {elem2})";

            Console.WriteLine(string.Join(", ", query));
        }
    }
}
