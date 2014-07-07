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
            Console.ReadKey();
            foreach (Product p in prods)
                Console.WriteLine(p.ToString());
            Console.ReadKey();
        }

        [TestMethod]
        public static void TestInsert(SqlMapper_v1.IDataMapper<Product> prodMapper)
        {
            Product newprod = new Product("benfas", "10", 12, 20, 0);
            prodMapper.Insert(newprod);
            UnitTest.TestGetAll(prodMapper); //vejo estado actual
        }

        [TestMethod]
        public static void TestDelete(SqlMapper_v1.IDataMapper<Product> prodMapper)
        {
            Console.WriteLine("Inseri um registo");
            UnitTest.TestInsert(prodMapper); //insiro registo
            SqlMapper_v1.DataMapper<Product> prodMapper2 = (SqlMapper_v1.DataMapper<Product>)prodMapper;
            int lastRecord = prodMapper2.GetLastInsertedRecord();
            UnitTest.TestInsert(prodMapper); //insiro registo
            Console.WriteLine("Inseri um registo");
            UnitTest.TestGetAll(prodMapper); //vejo estado actual
            Product newprod = new Product(lastRecord, "", "", 0, 0, 0);
            prodMapper.Delete(newprod);
            Console.WriteLine("Removi um registo");
            newprod = new Product(lastRecord + 1, "", "", 0, 0, 0);
            prodMapper.Delete(newprod);
            Console.WriteLine("Removi um registo");
            UnitTest.TestGetAll(prodMapper); //vejo estado actual
        }

        [TestMethod]
        public static void TestUpdate(SqlMapper_v1.IDataMapper<Product> prodMapper)
        {
            //UnitTest.TestGetAll(prodMapper); //vejo estado actual
            Product newprod = new Product(1,"NN", "10", 12, 20, 0);
            prodMapper.Update(newprod);
            Console.WriteLine("Alterei um registo");
            UnitTest.TestGetAll(prodMapper); //vejo estado actual
            newprod = new Product(1, "Batata", "5", 5, 25, 15);
            prodMapper.Update(newprod);
            Console.WriteLine("Repus o estado do registo");
            UnitTest.TestGetAll(prodMapper); //vejo estado actual            
        }

        [TestMethod]
        public static void TestGetAllv2(SqlMapper_v2.IDataMapper<Product> prodMapper)
        {
            SqlMapper_v2.ISqlEnumerable<Product> prods = prodMapper.GetAll();
            Console.WriteLine("Get all elements in Products:");
            foreach (Product p in prods)
                Console.WriteLine(p.ToString());
            prods = prodMapper.GetAll().Where("UnitsInStock > 15");
            Console.WriteLine("Get all elements in Products where «UnitsInStock > 15»:");
            foreach (Product p in prods)
                Console.WriteLine(p.ToString());
            prods = prodMapper.GetAll().Where("UnitsInStock > 15").Where("ProductName = 'benfas'");
            Console.WriteLine("Get all elements in Products where «UnitsInStock > 15» and «ProductName = 'benfas'»:");
            foreach (Product p in prods)
                Console.WriteLine(p.ToString());
            Console.ReadKey();
        }

        [TestMethod]
        public static void TestGetAllv3(SqlMapper_v3.IDataMapper mapper)
        {
            SqlMapper_v3.ISqlEnumerable result = mapper.GetAll();
            //Console.WriteLine("Get all elements in Orders:");
            foreach (var r in result)
                Console.WriteLine(r.ToString());
            Console.ReadKey();
        }
        
        [TestMethod]
        public static void TestInsertv3(SqlMapper_v3.IDataMapper orderMapper)
        {
            Console.WriteLine("Inseri um registo");
            Customer newcust = new Customer("C0001", "Company1", "Contact1", "Mr", "Rua xxx", "Porto", " North", "4950 ", " Portugal", " 91123456", "2151421 ");
            Employee newempl = new Employee(1, "Rodrigues", "Tatiana", "Ms", "Ms", DateTime.Now, DateTime.Now, "Rua xxx", "Porto", "North", "3521", "Portugal", "12387643", "521", null, " ", 1, " ");
            Order neworder = new Order(newcust, newempl, DateTime.Now, DateTime.Now, DateTime.Now, 4, 4, "Barco do amor", "miami", "miami", "miami", "333222", "miami");
            orderMapper.Insert(neworder);
            Console.ReadKey();//vejo estado actual
            Console.WriteLine("Inseri um registo");
            newcust = new Customer("C0002", "Company2", "Contact2", "Mrs", "Rua yy", "Porto", " North", "4950 ", " Portugal", " 91123456", "2151421 ");
            newempl = new Employee(5, "Domingos", "Diana", "Ms", "Ms", DateTime.Now, DateTime.Now, "Rua eee", "Porto", "North", "3521", "Portugal", "12387643", "521", null, " ", 1, " ");
            neworder = new Order(newcust, newempl, DateTime.Now, DateTime.Now, DateTime.Now, 4, 4, "Barco do amor", "miami", "miami", "miami","333222", "miami");
            orderMapper.Insert(neworder);
            Console.ReadKey();//vejo estado actual
        }

        [TestMethod]
        public static void TestDeletev3(SqlMapper_v3.IDataMapper orderMapper)
        {
            Console.WriteLine("Inseri um registo");
            Customer newcust = new Customer("C0001", "Company1", "Contact1", "Mr", "Rua xxx", "Porto", " North", "4950 ", " Portugal", " 91123456", "2151421 ");
            Employee newempl = new Employee(1, "Rodrigues", "Tatiana", "Ms", "Ms", DateTime.Now, DateTime.Now, "Rua xxx", "Porto", "North", "3521", "Portugal", "12387643", "521", null, " ", 1, " ");
            Order neworder = new Order(newcust, newempl, DateTime.Now, DateTime.Now, DateTime.Now, 4, 4, "Barco do amor", "miami", "miami", "miami", "333222", "miami");
            orderMapper.Insert(neworder);
            SqlMapper_v3.DataMapper<Order> orderMapper2 = (SqlMapper_v3.DataMapper<Order>)orderMapper;
            int lastRecord = orderMapper2.GetLastInsertedRecord();
            Console.WriteLine("Inseri um registo");
            newcust = new Customer("C0002", "Company2", "Contact2", "Mrs", "Rua yy", "Porto", " North", "4950 ", " Portugal", " 91123456", "2151421 ");
            newempl = new Employee(5, "Domingos", "Diana", "Ms", "Ms", DateTime.Now, DateTime.Now, "Rua eee", "Porto", "North", "3521", "Portugal", "12387643", "521", null, " ", 1, " ");
            neworder = new Order(newcust, newempl, DateTime.Now, DateTime.Now, DateTime.Now, 4, 4, "Barco do amor", "miami", "miami", "miami", "333222", "miami");
            orderMapper.Insert(neworder);
            Console.ReadKey();//vejo estado actual
            Order newremorder = new Order(lastRecord, newcust, newempl, DateTime.Now, DateTime.Now, DateTime.Now, 0, 0, "", "", "", "", "", "");
            orderMapper.Delete(newremorder);
            Console.WriteLine("Removi um registo");
            newremorder = new Order(lastRecord + 1, newcust, newempl, DateTime.Now, DateTime.Now, DateTime.Now, 0, 0, "", "", "", "", "", "");
            orderMapper.Delete(newremorder);
            Console.WriteLine("Removi um registo");
            Console.ReadKey();//vejo estado actual
        }

        [TestMethod]
        public static void TestUpdatev3(SqlMapper_v3.IDataMapper orderMapper)
        {
            Console.WriteLine("Alterei um registo numa Order");
            Customer newcust = new Customer("C0002", "Company2", "Contact2", "Mrs", "Rua yy", "Porto", " North", "4950 ", " Portugal", " 91123456", "2151421 ");
            Employee newempl = new Employee(5, "Domingos", "Diana", "Ms", "Ms", DateTime.Now, DateTime.Now, "Rua eee", "Porto", "North", "3521", "Portugal", "12387643", "521", null, " ", 1, " ");
            Order neworder = new Order(6, newcust, newempl, DateTime.Now, DateTime.Now, DateTime.Now, 4, 4, "Barco do amor", "miami", "miami", "miami", "333222", "miami");
            orderMapper.Update(neworder);
            Console.ReadKey();//vejo estado actual
            Console.WriteLine("Repus o estado do registo de uma Order");
            newcust = new Customer("C0001", "Company1", "Contact1", "Mr", "Rua xxx", "Porto", " North", "4950 ", " Portugal", " 91123456", "2151421 ");
            newempl = new Employee(1, "Rodrigues", "Tatiana", "Ms", "Ms", DateTime.Now, DateTime.Now, "Rua xxx", "Porto", "North", "3521", "Portugal", "12387643", "521", null, " ", 1, " ");
            neworder = new Order(6, newcust, newempl, DateTime.Now, DateTime.Now, DateTime.Now, 4, 4, "Barco do amor", "miami", "miami", "miami", "333222", "miami");
            orderMapper.Update(neworder);
            Console.ReadKey();//vejo estado actual            
        }

    }
}
