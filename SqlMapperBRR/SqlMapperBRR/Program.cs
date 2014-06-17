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
            try
            {
                con.ConnectionString = @"Data Source=DARKSTAR\SQLEXPRESS; 
                                        Initial Catalog=ave; 
                                        Integrated Security=True";
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandText = "SELECT * from Product";
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                //dr.GetDataTypeName
                //foreach (Object o in dr)
                //{
                //    //Console.WriteLine(dr["productName"]);
                //    Console.WriteLine(o.ToString());
                //}
                int count=0;


                while (dr.Read())
                {
                    Console.WriteLine(dr[count]);
                    Console.WriteLine(dr["productName"]);
                }

            }
            finally
            {
                if (con.State != ConnectionState.Closed)
                    con.Dispose();
                    //con.Close();
            }
        }
    }
}
