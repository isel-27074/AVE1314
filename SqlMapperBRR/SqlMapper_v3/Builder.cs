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



namespace SqlMapper_v3
{
    public class Builder
    {

        //uma connection string por instância
        private readonly SqlConnection _builderConnection;
        private string _table; //nome da tabela obtido no Build 
        private string[] _columnlist; //nomes das colunas obtido no Build

        //    public Builder(ConnectionPolicy cp, QueryData qd)
        public Builder(ConnectionPolicy cp)
        {
            string connectionString = "";
            connectionString = @"Data Source=" + cp.dataSource + "; ";
            connectionString = connectionString + "Initial Catalog=" + cp.initialCatalog + "; ";
            connectionString = connectionString + "Integrated Security=" + cp.integratedSecurity + "; ";
            connectionString = connectionString + "Connection Timeout=" + cp.connectionTimeout + "; ";
            connectionString = connectionString + "Pooling=" + cp.pooling + ";";
            _builderConnection = new SqlConnection(connectionString);

            //columnlist = qd.columns;
            //List<string> lista = qd.GetTables().ToList();
            //_table = lista.First();
        }

        public SqlConnection GetBuilderConnection()
        {
            return _builderConnection;
        }

        public IDataMapper<T> Build<T>() where T : class, new()
        {

            Type t = typeof(T);
            //Console.WriteLine("Class name: " + t.Name.ToString());
            //Console.ReadKey();

            Attribute[] attribs = Attribute.GetCustomAttributes(t);
            foreach (Attribute a in attribs)
            {
                if (a is TableAttribute)
                {
                    TableAttribute act = (TableAttribute)a;
                    //Console.WriteLine(act.Name);
                    _table = act.Name;
                }
            }

            //Gets all properties in class T
            PropertyInfo[] properties = t.GetProperties();
            //Gets all fields in class T
            FieldInfo[] fields = t.GetFields();

            _columnlist = new String[properties.Length + fields.Length];
            int idx = 0;
            foreach (PropertyInfo property in properties)
            {
                _columnlist[idx] = property.Name;
                idx++;
            }

            //Gets all fields in class T
            foreach (FieldInfo field in fields)
            {
                _columnlist[idx] = field.Name;
                idx++;
            }

            if (_builderConnection.State == ConnectionState.Open)
                Console.WriteLine("CON - Já estava aberta!");
            else
                Console.WriteLine("CON - Está fechada!");

            DataMapper<T> dm = new DataMapper<T>(_builderConnection, true, _table, _columnlist);
            //Console.WriteLine("Builder - Ending connection...");
            //if (_builderConnection.State != ConnectionState.Closed)
            //    _builderConnection.Dispose();

            return dm;
        }

    }
}
