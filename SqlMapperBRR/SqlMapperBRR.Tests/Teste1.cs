using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using System.Data.SqlClient;
//using System.Data;
//using DataModel;
//using SqlMapper_v1;

//using SqlMapper_v2;
//using SqlMapper_v3;

namespace SqlMapperBRR
{
    class Program
    {
        static void Main(string[] args)
        {
            //SqlConnection con = new SqlConnection();

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

            
            
            //con.ConnectionString = @"Data Source=" + datasource + "; Initial Catalog=ave; Integrated Security=True; Connection Timeout=5;";
            //SqlCommand cmd = con.CreateCommand();
            //cmd.CommandText = "SELECT * from Products";
            //Console.WriteLine("Openning connection...");
            //con.Open();

            ////Contem a ED (Tabela) e as suas colunas
            ////Dictionary<string, string[]> dic = new Dictionary<string, string[]>();
            ////string[] columns = new string[1];
            
            //////columns[0] = "ProductName";
            ////columns[0] = datasource2;
            ////dic.Add("Products", columns);
            ////QueryData qd = new QueryData(dic);

            //string icat = "ave", isec = "True", ctime="15", pooling="True";
            //ConnectionPolicy cp = new ConnectionPolicy(datasource, icat, isec, ctime, pooling);

            //Builder b = new Builder(cp);
            
            ////Builder b = new Builder(con.ConnectionString, "Products");
            //IDataMapper<Product> prodMapper = b.Build<Product>();

            ////SqlDataReader dr = cmd.ExecuteReader();

            ////dr.GetDataTypeName
            ////foreach (Object o in dr)
            ////{
            ////    //Console.WriteLine(dr["productName"]);
            ////    Console.WriteLine(o.ToString());
            ////}
            //int count = 0;
            //IEnumerable<Product> prods = prodMapper.GetAll();
            //foreach (Product p in prods)
            //    Console.WriteLine(p.ToString());

            ////prodMapper.GetAll2();
            ////Console.ReadKey();
            //prodMapper.GetAll
            //while (b.GetSqlDataReader().Read())
            //{
            //    Console.WriteLine(b.GetSqlDataReader()[count]);
            //    Console.WriteLine(b.GetSqlDataReader()["productName"]);
            //}

            
           
            //Console.WriteLine("Ending connection...");
            Console.ReadKey();
            //SqlConnection teste = b.getBuilderConnection();

        }
    }
}
