using System;
using System.Collections.Generic;

namespace LinqExercises
{
    public static class ProductsDatabase
    {
        private static Func<int> Increment()
        {
            int id = 0;
            return () => ++id;
        }

        private static Func<int> NewId = Increment();

        private static List<Category> categories = new List<Category>();

        private static List<Product> products = new List<Product>();

        public static IEnumerable<Category> Categories
        {
            get
            {
                if (ProductsDatabase.categories.Count == 0)
                {
                    ProductsDatabase.categories.AddRange(ProductsDatabase.GenerateAllCategories());
                }

                return ProductsDatabase.categories;
            }
        }

        public static IEnumerable<Product> Products
        {
            get
            {
                if (ProductsDatabase.products.Count == 0)
                {
                    ProductsDatabase.products.AddRange(ProductsDatabase.GenerateAllProducts());
                }

                return ProductsDatabase.products;
            }
        }

        private static IEnumerable<Category> GenerateAllCategories()
        {
            yield return new Category(1, "Laptopuri");
            yield return new Category(2, "Telefoane");
            yield return new Category(3, "Tablete");
        }

        private static IEnumerable<Product> GenerateAllProducts()
        {
            // Laptopuri
            yield return new Product(NewId(), "Lenovo IdeaPad", 1);
            yield return new Product(NewId(), "HP Envy", 1);
            yield return new Product(NewId(), "Dell Latitude", 1);

            // Telefoane
            yield return new Product(NewId(), "Samsung Galaxy Phone", 2);
            yield return new Product(NewId(), "Huawei Phone", 2);
            yield return new Product(NewId(), "Xiaomi Phone", 2);
            yield return new Product(NewId(), "Nokia Phone", 2);
            yield return new Product(NewId(), "iPhone", 2);

            // Tablete
            yield return new Product(NewId(), "Samsung Galaxy Tab", 3);
            yield return new Product(NewId(), "Huawei Tablet", 3);
            yield return new Product(NewId(), "Lenovo Tablet", 3);
            yield return new Product(NewId(), "iPad", 3);
        }
    }
}
