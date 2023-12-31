﻿using Business.Concrete;
using DataAccess.Concrete.EntityFreamwork;
using DataAccess.Concrete.InMemory;

namespace ConsoleUI
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Data Transformation Object 
            //CategoryTest();
            //IoC
            //ProductTest();

            

        }

        private static void CategoryTest()
        {
            CategoryManager categoryManager = new CategoryManager(new EfCategoryDal());
            foreach (var category in categoryManager.GetAll().Data)
            {
                Console.WriteLine(category.CategoryName);
            }
        }

        private static void ProductTest()
        {
            ProductManager productManager = new ProductManager(new EfProductDal(),
                new CategoryManager(new EfCategoryDal()));

            var result = productManager.GetProductDetails();
            if (result.Success)
            {
                foreach (var product in result.Data)
                {
                    Console.WriteLine(product.ProductName + " / " + product.CategoryName);
                }
            }
            else
            {
                Console.WriteLine(result.Message);
            }

            
        }
    }
}