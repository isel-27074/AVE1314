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
        private string _table;
        private SqlDataReader _dr;

        public Builder(string connection, string table) {
            _builderConnection = new SqlConnection();
            _builderConnection.ConnectionString = connection;
            _table = table;
        }

        public SqlConnection GetBuilderConnection() {
            return _builderConnection;
        }

        public SqlDataReader GetSqlDataReader()
        {
            return _dr;
        }

        public void Open()
        {


        }

        public IDataMapper<T> Build<T>() where T : class
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
                if (_builderConnection.State != ConnectionState.Closed)
                    _builderConnection.Dispose();
            }
            return null;
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
