using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using DataModel;
using SqlMapper_v1;
using SqlMapper_v2;
using SqlMapper_v3;
using SqlMapperBRR.Tests;
using System.Reflection;

namespace SqlMapperBRR
{
    class Program
    {
        static void Main(string[] args)
        {

            #region menu
            string datasource = null;

            while (datasource == null || datasource.Equals(""))
            {
                Console.WriteLine("Escolha Data Source:");
                Console.WriteLine("A - PCK8");
                Console.WriteLine("B - Darkstar\\Sqlexpress");
                Console.WriteLine("C - FONTAINHA\\SQLEXPRESS");
                Console.WriteLine("D - Introduzir nova DS");
                datasource = Console.ReadLine();
                switch (datasource.ToUpper())
                {
                    case "A":
                        datasource = "PCK8";
                        break;
                    case "B":
                        datasource = "darkstar\\sqlexpress";
                        break;
                    case "C":
                        datasource = "FONTAINHA\\SQLEXPRESS";
                        break;
                    default:
                        Console.WriteLine("Inserir Data Source:");
                        datasource = Console.ReadLine();
                        break;

                }
            }

            Console.WriteLine(datasource);
            #endregion menu


            Dictionary<string, string[]> dic = new Dictionary<string, string[]>();

            string icat = "ave", isec = "True", ctime = "15", pooling = "True";
            bool commitable = false;
            #region SqlMapper_v1
            dic.Add("Products", new string[] { "ProductID", "ProductName", "QuantityPerUnit", "UnitPrice", "UnitsInStock", "UnitsOnOrder" });
            SqlMapper_v1.ConnectionPolicy cpv1 = new SqlMapper_v1.ConnectionPolicy(datasource, icat, isec, ctime, pooling, commitable);
            SqlMapper_v1.QueryData qdv1 = new SqlMapper_v1.QueryData(dic);
            SqlMapper_v1.Builder bv1 = new SqlMapper_v1.Builder(cpv1, qdv1);
            SqlMapper_v1.IDataMapper<Product> prodMapperv1 = bv1.Build<Product>();

            //Test GetAll
            //UnitTest.TestGetAll(prodMapperv1);

            //Test Insert
            //UnitTest.TestInsert(prodMapperv1);
            //UnitTest.TestGetAll(prodMapperv1);

            ////Test Delete
            //UnitTest.TestDelete(prodMapperv1);
            //UnitTest.TestGetAll(prodMapperv1);

            ////Test Update
            //UnitTest.TestUpdate(prodMapperv1);
            //UnitTest.TestGetAll(prodMapperv1);

            #endregion


            #region SqlMapper_v2
            SqlMapper_v2.ConnectionPolicy cpv2 = new SqlMapper_v2.ConnectionPolicy(datasource, icat, isec, ctime, pooling);
            SqlMapper_v2.Builder bv2 = new SqlMapper_v2.Builder(cpv2);
            SqlMapper_v2.IDataMapper<Product> prodMapperv2 = bv2.Build<Product>();
            //Test GetAll
            //UnitTest.TestGetAllv2(prodMapperv2);

            #endregion


            #region SqlMapper_v3
            SqlMapper_v3.ConnectionPolicy cpv3 = new SqlMapper_v3.ConnectionPolicy(datasource, icat, isec, ctime, pooling);
            SqlMapper_v3.Builder bv3 = new SqlMapper_v3.Builder(cpv3);
            //SqlMapper_v3.IDataMapper prodMapperv3 = bv3.Build<Product>();
            //ISqlEnumerable prod = prodMapperv3.GetAll();
            SqlMapper_v3.IDataMapper orderMapperv3 = bv3.Build<Order>();
            ISqlEnumerable order = orderMapperv3.GetAll();
            //SqlMapper_v3.IDataMapper custMapperv3 = bv3.Build<Customer>();
            //ISqlEnumerable cust = custMapperv3.GetAll();
            //SqlMapper_v3.IDataMapper emplMapperv3 = bv3.Build<Employee>();
            //ISqlEnumerable empl = emplMapperv3.GetAll();

            //Test GetAll
            UnitTest.TestGetAllv3(orderMapperv3);

            #endregion

            ////TESTES
            //IEnumerable<Product> prods = prodMapper.GetAll();

            //foreach (Product p in prods)
            //    Console.WriteLine(p.ToString());

            //Console.ReadKey();

            //Product newprod = new Product("produto", "10", 12, 20, 0);

            //prodMapper.Insert(newprod);

            //Console.ReadKey();

            //prods = prodMapper.GetAll();

            //foreach (Product p in prods)
            //    Console.WriteLine(p.ToString());

            //Console.ReadKey();
        }
    }
}
