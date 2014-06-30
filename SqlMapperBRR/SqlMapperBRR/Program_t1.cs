using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using DataModel;
using SqlMapper_v1;
//using SqlMapper_v2;
//using SqlMapper_v3;

namespace SqlMapperBRR
{
    class Program_t1
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
                Console.WriteLine("C - UTILIZADOR-PC ");
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
                        datasource = "UTILIZADOR-PC";
                        break;
                    default:
                        Console.WriteLine("Inserir Data Source:");
                        datasource = Console.ReadLine();
                        break;

                }
            }

            Console.WriteLine(datasource);
            #endregion menu


            string icat = "ave", isec = "True", ctime="15", pooling="True";
            ConnectionPolicy cp = new ConnectionPolicy(datasource, icat, isec, ctime, pooling);

            Builder b = new Builder(cp);
            
            IDataMapper<Product> prodMapper = b.Build<Product>();

            IEnumerable<Product> prods = prodMapper.GetAll();

            foreach (Product p in prods)
                Console.WriteLine(p.ToString());

            Console.ReadKey();
            //ProductID, ProductName, QuantityPerUnit, UnitPrice, UnitsInStock, UnitsOnOrder
            Product newprod = new Product(0, "produto", "10", 12, 20, 0);

            prodMapper.Insert(newprod);

            prods = prodMapper.GetAll();

            foreach (Product p in prods)
                Console.WriteLine(p.ToString());

            Console.ReadKey();
        }
    }
}
