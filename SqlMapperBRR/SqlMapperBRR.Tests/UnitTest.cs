﻿using System;
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
            Product newprod = new Product("benfas", "10", 12, 20, 0);

            prodMapper.Insert(newprod);
        }

        [TestMethod]
        public static void TestDelete(IDataMapper<Product> prodMapper)
        {
            UnitTest.TestGetAll(prodMapper); //vejo estado actual
            UnitTest.TestInsert(prodMapper); //insiro registo
            DataMapper<Product> prodMapper2 = (DataMapper<Product>)prodMapper;
            int lastRecord = prodMapper2.GetLastInsertedRecord();
            Product newprod = new Product(11, "", "", 0, 0, 0);
            prodMapper.Delete(newprod);
        }

        [TestMethod]
        public static void TestUpdate(IDataMapper<Product> prodMapper)
        {
            Product newprod = new Product(5,"NN", "10", 12, 20, 0);
            prodMapper.Update(newprod);
        }
    }
}
