using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SqlMapper_v1;
using SqlMapper_v2;
//using SqlMapper_v3;
using DataModel;
using System.Collections.Generic;

namespace SqlMapperBRR.Tests
{
    [TestClass]
    public static class UnitTest
    {
        [TestMethod]
        public static void TestGetAll(SqlMapper_v1.IDataMapper<Product> prodMapper)
        {
            IEnumerable<Product> prods = prodMapper.GetAll();

            foreach (Product p in prods)
                Console.WriteLine(p.ToString());

            Console.ReadKey();
        }

        [TestMethod]
        public static void TestInsert(SqlMapper_v1.IDataMapper<Product> prodMapper)
        {
            Product newprod = new Product("benfas", "10", 12, 20, 0);
            prodMapper.Insert(newprod);
        }

        [TestMethod]
        public static void TestDelete(SqlMapper_v1.IDataMapper<Product> prodMapper)
        {
            UnitTest.TestInsert(prodMapper); //insiro registo
            SqlMapper_v1.DataMapper<Product> prodMapper2 = (SqlMapper_v1.DataMapper<Product>)prodMapper;
            int lastRecord = prodMapper2.GetLastInsertedRecord();
            UnitTest.TestInsert(prodMapper); //insiro registo
            UnitTest.TestGetAll(prodMapper); //vejo estado actual
            Product newprod = new Product(lastRecord, "", "", 0, 0, 0);
            prodMapper.Delete(newprod);
        }

        [TestMethod]
        public static void TestUpdate(SqlMapper_v1.IDataMapper<Product> prodMapper)
        {
            Product newprod = new Product(1,"NN", "10", 12, 20, 0);
            prodMapper.Update(newprod);
        }

        [TestMethod]
        public static void TestGetAllv2(SqlMapper_v2.IDataMapper<Product> prodMapper)
        {
            SqlMapper_v2.ISqlEnumerable<Product> prods = prodMapper.GetAll();
            Console.WriteLine("Get all elements in Products:");
            foreach (Product p in prods)
                Console.WriteLine(p.ToString());
            prods = prodMapper.GetAll().Where("UnitPrice = 12");
            Console.WriteLine("Get all elements in Products where «UnitPrice = 12»:");
            foreach (Product p in prods)
                Console.WriteLine(p.ToString());
            prods = prodMapper.GetAll().Where("UnitPrice = 12").Where("ProductName = 'benfas'");
            Console.WriteLine("Get all elements in Products where «UnitPrice = 12» and «ProductName = 'benfas'»:");
            foreach (Product p in prods)
                Console.WriteLine(p.ToString());
            Console.ReadKey();
        }

        [TestMethod]
        public static void TestGetAllv3(SqlMapper_v3.IDataMapper orderMapper)
        {
            SqlMapper_v3.ISqlEnumerable orders = orderMapper.GetAll();
            Console.WriteLine("Get all elements in Orders:");
            foreach (var o in orders)
                Console.WriteLine(o.ToString());
            //orders = prodMapper.GetAll().Where("");
            //Console.WriteLine("Get all elements in Products where «UnitPrice = 12»:");
            //foreach (var p in orders)
            //    Console.WriteLine(p.ToString());
            //orders = prodMapper.GetAll().Where("").Where("");
            //Console.WriteLine("Get all elements in Products where «UnitPrice = 12» and «ProductName = 'benfas'»:");
            //foreach (var p in orders)
            //    Console.WriteLine(p.ToString());
            Console.ReadKey();
        }


        [TestMethod]
        public static void TestInsert3(SqlMapper_v3.IDataMapper orderMapper)
        {
            //Product newprod = new Product("benfas", "10", 12, 20, 0);
            Customer newcust = new Customer("C0002", "Company2", "Contact2", "Mrs", "Rua yy", "Porto", " North", "4950 ", " Portugal", " 91123456", "2151421 ");
            Order neworder = new Order(newcust, 1, System.DateTime.Now, System.DateTime.Now, System.DateTime.Now,
            4, 4, "Barco do amor", "miami", "miami", "miami","333222", "miami");
            orderMapper.Insert(neworder);
        }

    }
}
