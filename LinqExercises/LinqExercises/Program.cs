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

        private static void OrderBy_Basic()
        {
            //var query = PersonsDatabase.AllPersons()
            //    .Where(p => (p.Age > 20) && (p.Age < 40))
            //    .OrderBy(p => p.Age)
            //    .ThenByDescending(p => p.FullName);

            var query = from p in PersonsDatabase.AllPersons()
                        where (p.Age > 20) && (p.Age < 40)
                        orderby p.Age ascending, p.FullName descending
                        select p;

            foreach (Person p in query)
            {
                p.Print();
            }
        }

        private static void GroupBy_Basic()
        {
            var query = from p in PersonsDatabase.AllPersons()
                        where p.Age > 30
                        orderby p.DateOfBirth.Year ascending
                        group p by p.DateOfBirth.Year into yearsGroups
                        where (yearsGroups.Key >= 1950) && (yearsGroups.Key <= 1970)
                        select yearsGroups;

            //var query = PersonsDatabase.AllPersons()
            //    .Where(p => p.Age > 30)
            //    .OrderBy(p => p.DateOfBirth.Year)
            //    .GroupBy(p => p.Age);

            foreach (var group in query)
            {
                Console.WriteLine("--------------");
                Console.WriteLine($"Persons born in year: {group.Key}");
                Console.WriteLine("--------------");

                foreach (var p in group)
                {
                    p.Print();
                }
            }
        }

        private static void Union_WithCustomEquality()
        {
            var p1 = new Person("James", "Smith", new DateTime(1975, 2, 24), Gender.Male);
            var p2 = new Person("Mary", "Brown", new DateTime(1985, 2, 24), Gender.Female);
            var p3 = new Person("James", "Smith", new DateTime(1975, 2, 24), Gender.Male);
            var p4 = new Person("Nancy", "Jones", new DateTime(1991, 2, 24), Gender.Female);

            Person[] persons1 = new Person[] { p1, p2 };

            Person[] persons2 = new Person[] { p3, p4 };

            //var query = persons1.Union(
            //    persons2,
            //    new PersonsEqualityComparer());

            var query = persons1.Union(
                persons2,
                new LambdaEqualityComparer<Person>(
                    (x, y) => string.Equals(x.FirstName, y.FirstName) &&
                              string.Equals(x.LastName, y.LastName) &&
                              (x.DateOfBirth == y.DateOfBirth) &&
                              (x.Gender == y.Gender),
                    x => HashCode.Combine(x.FirstName, x.LastName, x.DateOfBirth, x.Gender)));

            foreach (var p in query)
            {
                p.Print();
            }

            int[] ints1 = { 1, 2, 3 };
            int[] ints2 = { 2, 3, 4 };

            var query2 = ints1.Union(ints2);
            Console.WriteLine(string.Join(", ", query2));
        }

        private static void DefaultIfEmpty_Basic()
        {
            var p1 = new Person("James", "Smith", new DateTime(1975, 2, 24), Gender.Male);
            var p2 = new Person("Mary", "Brown", new DateTime(1985, 2, 24), Gender.Female);
            var p3 = new Person("James", "Smith", new DateTime(1975, 2, 24), Gender.Male);
            var p4 = new Person("Nancy", "Jones", new DateTime(1991, 2, 24), Gender.Female);

            // Person[] persons1 = null;
            Person[] persons2 = new Person[] { };
            Person[] persons3 = new Person[] { p1, p2, p3, p4 };

            foreach (var arrayOfPersons in new[] { persons2, persons3 })
            {
                var query = arrayOfPersons.DefaultIfEmpty();
                foreach (var p in query)
                {
                    if (p is null)
                    {
                        Console.WriteLine("p is null");
                    }
                    else
                    {
                        p.Print();
                    }

                }

                Console.WriteLine(" --------------------------------- ");
            }

            int[] ints1 = new int[0];
            int[] ints2 = { 1, 2, 3, 4 };
            foreach (var arrayOfInts in new[] { ints1, ints2 })
            {
                var query = arrayOfInts.DefaultIfEmpty();
                Console.WriteLine(string.Join(", ", query));
                Console.WriteLine(" --------------------------------- ");
            }
        }

        private static void Zip_Vs_SelectMany()
        {
            int[] array1 = { 1, 2, 3, };
            string[] array2 = { "one", "two", "three", "four", "five" };

            var query1 = array1.Zip(array2, (first, second) => $"{first} {second}");
            foreach (var element in query1)
            {
                Console.WriteLine(element);
            }

            Console.WriteLine(" ------------------------------- ");

            var query2 = array1.SelectMany(
                (first) => array2,
                (first, second) => $"{first} {second}");

            foreach (var element in query2)
            {
                Console.WriteLine(element);
            }
        }

        private static void Join_Basic()
        {
            var query = from product in ProductsDatabase.Products
                        join category in ProductsDatabase.Categories on product.CategoryId equals category.Id
                        select new
                        {
                            product.Id,
                            ProductName = product.Name,
                            CategoryName = category.Name
                        };

            var queryExt = ProductsDatabase.Products
                .Join(
                    ProductsDatabase.Categories,
                    prod => prod.CategoryId,
                    cat => cat.Id,
                    (prod, cat) => new
                    {
                        prod.Id,
                        ProductName = prod.Name,
                        CategoryName = cat.Name
                    });


            foreach (var result in query)
            {
                Console.WriteLine($"{result.Id} - {result.ProductName} (category: {result.CategoryName})");
            }

            Console.WriteLine("------------------------------------");

            foreach (var result in queryExt)
            {
                Console.WriteLine($"{result.Id} - {result.ProductName} (category: {result.CategoryName})");
            }
        }

        private static void Join_GroupJoin()
        {
            var query = from category in ProductsDatabase.Categories
                        join product in ProductsDatabase.Products on category.Id equals product.CategoryId into categoryGroup
                        select new
                        {
                            category.Id,
                            CategoryName = category.Name,
                            Products = categoryGroup
                        };

            var queryExt = ProductsDatabase.Categories.GroupJoin(
                ProductsDatabase.Products,
                cat => cat.Id,
                prod => prod.CategoryId,
                (cat, products) => new
                {
                    cat.Id,
                    CategoryName = cat.Name,
                    Products = products
                });

            foreach (var result in query)
            {

                // Employee
                // Project
                // Employee - to - project n - m
                Console.WriteLine($"{result.Id} Category: {result.CategoryName}");
                foreach (var product in result.Products)
                {
                    Console.WriteLine($"    - {product.Id} - {product.Name}");
                }
            }

            Console.WriteLine("------------------------------------");

            foreach (var result in queryExt)
            {
                Console.WriteLine($"{result.Id} Category: {result.CategoryName}");
                foreach (var product in result.Products)
                {
                    Console.WriteLine($"    - {product.Id} - {product.Name}");
                }
            }
        }

        private static void Join_OuterJoinSimulation()
        {
            var query = from product in ProductsDatabase.Products
                        join category in ProductsDatabase.Categories on product.CategoryId equals category.Id into categoriesGroup
                        from cat in categoriesGroup.DefaultIfEmpty(new Category(-1, "N/A"))
                        select new
                        {
                            product.Id,
                            ProductName = product.Name,
                            CategoryName = cat.Name
                        };

            foreach (var result in query)
            {
                Console.WriteLine($"{result.Id} - {result.ProductName} (category: {result.CategoryName})");
            }
        }
    }
}
