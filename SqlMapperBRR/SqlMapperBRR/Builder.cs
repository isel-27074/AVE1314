using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace SqlMapperBRR
{
    public class Builder
    {
        /* connectionString="
           Data Source=PCK8\MSSQLSERVER;
           Initial Catalog=ave;
           Integrated Security=True"
        */

        //uma connection string por instância
        private readonly SqlConnection _builderConnection;
        private string _table;
        private SqlDataReader _dr;

        public Builder(string connection, string table) {
            _builderConnection = new SqlConnection();
            _builderConnection.ConnectionString = connection;
            _table = table;
        }

        public Builder(string table)
        {
            _builderConnection = new SqlConnection();
            String connectionString = @"Data Source=PCK8; Initial Catalog=ave; Integrated Security=True";
            _builderConnection.ConnectionString = connectionString;
            _table = table;
        }

        public SqlConnection getBuilderConnection() {
            return _builderConnection;
        }

        //public IDataMapper<T> Build<T>() {
        //    throw new NotImplementedException();
        //    //return null;
        //}

        public IDataMapper<T> Build<T>()
        {
            try
            {
                Type t = typeof(T);
                object[] allAtributes = t.GetCustomAttributes(typeof(SQLTableName), true);

                
                SqlCommand cmd = _builderConnection.CreateCommand();
                cmd.CommandText = "SELECT * from " + _table;
                _builderConnection.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                int count = 0;

                //while (dr.Read())
                //{
                //    Console.WriteLine(dr["productName"]);
                //    yield return dr[count++];
                //}
                DataMapper<Product> dm = new DataMapper<Product>(dr);
                //IDataMapper<T> id = new Builder<T>();
                return dm;
            }
            finally
            {
                if (_builderConnection.State != ConnectionState.Closed)
                    _builderConnection.Dispose();
                //con.Close();
            }
            //throw new NotImplementedException();
        }

        //public IEnumerable<T> GetAll()
        //{
        //    int count=0;
        //    while (_dr.Read())
        //        yield return (T)_dr[count++];
            
        //    //return (IEnumerable<T>)dataReader;

        //}

        //public void Update(T val)
        //{
        //    throw new NotImplementedException();
        //}

        //public void Delete(T val)
        //{
        //    throw new NotImplementedException();
        //}

        //public void Insert(T val)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
