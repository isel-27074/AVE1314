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



namespace SqlMapper_v2
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
            //_builderConnection = new SqlConnection();
            string connectionString = "";
            connectionString = @"Data Source=" + cp.dataSource + "; ";
            connectionString = connectionString + "Initial Catalog=" + cp.initialCatalog + "; ";
            connectionString = connectionString + "Integrated Security=" + cp.integratedSecurity + "; ";
            connectionString = connectionString + "Connection Timeout=" + cp.connectionTimeout + "; ";
            connectionString = connectionString + "Pooling=" + cp.pooling + ";";
            _builderConnection = new SqlConnection(connectionString);
            
            //_builderConnection.ConnectionString = connectionString;
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
            Console.WriteLine("Class name: " + t.Name.ToString());
            Console.ReadKey();

            Attribute[] attribs = Attribute.GetCustomAttributes(t);
            foreach (Attribute a in attribs)
            {
                if (a is TableAttribute) {
                    TableAttribute act = (TableAttribute) a;
                    //Console.WriteLine(act.Name);
                    _table = act.Name;
                }
            }

            //Gets all properties in class T
            PropertyInfo[] props = t.GetProperties();
            _columnlist = new String[props.Length];
            int i = 0;
            foreach (PropertyInfo prop in props)
            {
                //Console.WriteLine(prop.Name); //to remove
                _columnlist[i] = prop.Name;
            }

            //Gets all fields in class T
            FieldInfo[] fields = t.GetFields();
            foreach (FieldInfo field in fields)
            {
                //Console.WriteLine(field.Name); //to remove
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
