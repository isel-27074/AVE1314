using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace SqlMapperBRR
{
    class Program
    {
        static void Main(string[] args)
        {
            SqlConnection con = new SqlConnection();

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
            try
            {
                con.ConnectionString = @"Data Source=" + datasource + "; Initial Catalog=ave; Integrated Security=True";
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandText = "SELECT * from Product";
                Console.WriteLine("Openning connection...");
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                //dr.GetDataTypeName
                //foreach (Object o in dr)
                //{
                //    //Console.WriteLine(dr["productName"]);
                //    Console.WriteLine(o.ToString());
                //}
                int count = 0;


                while (dr.Read())
                {
                    Console.WriteLine(dr[count]);
                    Console.WriteLine(dr["productName"]);
                }
            }
            catch (SqlException se) {
                Console.WriteLine(se.Message);
            }

            finally
            {
                Console.WriteLine("Ending connection...");
                Console.ReadKey(); 
                if (con.State != ConnectionState.Closed)
                    con.Dispose();
                    //con.Close();
            }
        }
    }
}
