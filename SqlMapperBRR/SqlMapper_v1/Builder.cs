using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;



namespace SqlMapper_v1
{
    public class Builder
    {

        //uma connection string por instância
        private readonly SqlConnection _builderConnection;
        //private string _table; //nome da tabela ???
        //private string[] columnlist; //nomes das colunas ???

    //    public Builder(ConnectionPolicy cp, QueryData qd)
        public Builder(ConnectionPolicy cp)
        {
            _builderConnection = new SqlConnection();
            string connectionString = "";
            connectionString = @"Data Source=" + cp.dataSource + "; ";
            connectionString = connectionString + "Initial Catalog=" + cp.initialCatalog + "; ";
            connectionString = connectionString + "Integrated Security=" + cp.integratedSecurity + "; ";
            connectionString = connectionString + "Connection Timeout=" + cp.connectionTimeout + "; ";
            connectionString = connectionString + "Pooling=" + cp.pooling + ";";
            _builderConnection.ConnectionString = connectionString;
            //columnlist = qd.columns;
            //List<string> lista = qd.GetTables().ToList();
            //_table = lista.First();
        }

        public SqlConnection GetBuilderConnection() {
            return _builderConnection;
        }

        public IDataMapper<T> Build<T>() where T : class, new()
        {
            /* ver p nome da tabela:
             * ir buscar o nome anotado como atributo "table"
             * nomes das colunas da classe:
             *  getfiels ou getproperties
             *  criar instancia de datamaper correspondente à tabela em causa:
             *  
             * 
             */

            Type t = typeof(T);
            Console.WriteLine(t.Name.ToString());
            Console.ReadKey();


            Attribute[] attribs = Attribute.GetCustomAttributes(t);
            foreach (Attribute a in attribs)
            {
                
                if (a is TableAttribute) {
                    TableAttribute act = (TableAttribute)a;
                    Console.WriteLine(act.Name);
                }
            }
            PropertyInfo[] props = t.GetProperties();
            FieldInfo[] fields = t.GetFields();
            //foreach (PropertyInfo prop in props)
            //{
                //object[] attrs = prop.GetCustomAttributes(true);
                //foreach (object attr in attrs)
                //{
                //    TableAttribute authAttr = attr as TableAttribute;
                //    if (authAttr != null)
                //    {
                //        string propName = prop.Name;
                //        string auth = authAttr.Name;
                //        Console.WriteLine(propName);
                //        Console.WriteLine(auth);
                //    }
                //}
            //}

            //var MyAttribute = Attribute.GetCustomAttribute(t, typeof(TableAttribute));
            //Attribute[] oo = Attribute.GetCustomAttributes(t);
            //Object ooo = t.GetCustomAttributes(typeof(TableAttribute), true);
            //Console.WriteLine(MyAttribute.);
            //Console.WriteLine(MyAttribute.TypeId.ToString());
            //foreach (object o in oo)
            //{
            //   //Console.WriteLine(o.ToString());
            //    a = (Attribute)o;

            //    Console.WriteLine(a.GetType().GetProperties().GetValue(0).);
            //    Console.WriteLine(a.GetType().GetProperties().GetValue(1));
            //    Console.WriteLine(a.GetType().GetProperties().GetValue(2));
            //    Console.WriteLine(a.GetType().Name);
            //}
            //try
            //{typeof(Table),true t.GetCustomAttributes(typeof(TableAttribute), true);
            //    //Type t = typeof(T);
            //    //object[] allAtributes = t.GetCustomAttributes(typeof(SQLTableName), true);
            //    //Console.WriteLine(t.ToString());
                

            //}
            //catch (SqlException se) {
            //    Console.WriteLine(se.Message);
            //}
            //finally
            //{
            //    Console.WriteLine("Builder - Ending connection...");
            //    //if (_builderConnection.State != ConnectionState.Closed)
            //        //_builderConnection.Dispose();
            //}
            return null;
        }

    }
}
