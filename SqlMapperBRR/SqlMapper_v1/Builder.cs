using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace SqlMapper_v1
{
    public class Builder
    {

        //uma connection string por instância
        private readonly SqlConnection _builderConnection;
        private string _table; //testes
        private SqlDataReader _dr;

        //public Builder(string connection, string table) {
        //    _builderConnection = new SqlConnection();
        //    _builderConnection.ConnectionString = connection;
        //    _table = table;
        //}

        public Builder(ConnectionPolicy cp, QueryData qd)
        {
            _builderConnection = new SqlConnection();
            string connectionString = "";
            connectionString = @"Data Source=" + cp.dataSource + "; ";
            connectionString = connectionString + "Initial Catalog=" + cp.initialCatalog + "; ";
            connectionString = connectionString + "Integrated Security=" + cp.integratedSecurity + "; ";
            connectionString = connectionString + "Connection Timeout=" + cp.connectionTimeout + "; ";
            connectionString = connectionString + "Pooling=" + cp.pooling + ";";
            _builderConnection.ConnectionString = connectionString;

            List<string> lista = qd.GetTables().ToList();
            _table = lista.First();
        }

        public SqlConnection GetBuilderConnection() {
            return _builderConnection;
        }

        public SqlDataReader GetSqlDataReader() {
            return _dr;
        }

        public void GetCSdata()
        {
            Console.WriteLine("mySqlConnection.ConnectionString = " + _builderConnection.ConnectionString);
            Console.WriteLine("mySqlConnection.ConnectionTimeout = " + _builderConnection.ConnectionTimeout);
            Console.WriteLine("mySqlConnection.Database = " + _builderConnection.Database);
            Console.WriteLine("mySqlConnection.DataSource = " + _builderConnection.DataSource);
            Console.WriteLine("mySqlConnection.PacketSize = " + _builderConnection.PacketSize);
            Console.WriteLine("mySqlConnection.ServerVersion = " + _builderConnection.ServerVersion);
            Console.WriteLine("mySqlConnection.State = " + _builderConnection.State);
            Console.WriteLine("mySqlConnection.WorkstationId = " + _builderConnection.WorkstationId);
        }

        public void Open()
        {


        }

        public IDataMapper<T> Build<T>() where T : class, new()
        {
            try
            {
                //Type t = typeof(T);
                //object[] allAtributes = t.GetCustomAttributes(typeof(SQLTableName), true);
                //Console.WriteLine(t.ToString());
                
                SqlCommand cmd = _builderConnection.CreateCommand();
                cmd.CommandText = "SELECT * from " + _table;
                _builderConnection.Open();
                Console.WriteLine("Builder - Openning connection...");
                _dr = cmd.ExecuteReader();
                DataMapper<T> dm = new DataMapper<T>(_dr);
                //return (IDataMapper<T>)dm;
                return dm;
            }
            catch (SqlException se) {
                Console.WriteLine(se.Message);
            }
            finally
            {
                Console.WriteLine("Builder - Ending connection...");
                //if (_builderConnection.State != ConnectionState.Closed)
                    //_builderConnection.Dispose();
            }
            return null;
        }

    }
}
