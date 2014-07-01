using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SqlMapper_v1;
using DataModel;
using System.Collections.Generic;

namespace SqlMapperBRR.Tests
{
    [TestClass]
    public static class UnitTest
    {
        [TestMethod]
        public static void TestGetAll(IDataMapper<Product> prodMapper)
        {
            IEnumerable<Product> prods = prodMapper.GetAll();

            foreach (Product p in prods)
                Console.WriteLine(p.ToString());

            Console.ReadKey();
        }

        [TestMethod]
        public static void TestInsert(IDataMapper<Product> prodMapper)
        {
            //ProductID - desnecessário
            //ProductName, QuantityPerUnit, UnitPrice, UnitsInStock, UnitsOnOrder
            Product newprod = new Product("produto", "10", 12, 20, 0);

            prodMapper.Insert(newprod);
        }

        public static void TestDelete(IDataMapper<Product> prodMapper, int lastRecord)
        {
            //ProductID - desnecessário
            //ProductName, QuantityPerUnit, UnitPrice, UnitsInStock, UnitsOnOrder
            Product newprod = new Product(lastRecord, "produto", "10", 12, 20, 0);
            prodMapper.Delete(newprod);
        }
    }
}
