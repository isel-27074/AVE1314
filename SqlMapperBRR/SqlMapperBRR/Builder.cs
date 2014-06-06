using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace SqlMapperBRR
{
    public class Builder<T>:IDataMapper<T> where T : class
    {
        /* connectionString="
         * Data Source=«Computer»\«Instance»;
         * Initial Catalog=«DBName»;
         * Integrated Security=True"
        */

        //uma connection string por instância
        private readonly SqlConnection _builderConnection;
        private string _table;

        public Builder(string connection, string table) {
            _builderConnection = new SqlConnection();
            _builderConnection.ConnectionString = connection;
            _table = table;
        }

        public SqlConnection getBuilderConnection() {
            return _builderConnection;
        }

        public IDataMapper<T> Build<T>() {
            throw new NotImplementedException();
        }

        public IEnumerable<T> GetAll()
        {
            SqlDataReader dataReader = null;
            try
            {
                _builderConnection.Open();
                SqlCommand builderCommand = _builderConnection.CreateCommand();
                builderCommand.CommandText = "SELECT * FROM " + _table;
                dataReader = builderCommand.ExecuteReader();
            }            finally {                if (_builderConnection.State != ConnectionState.Closed) 
                    _builderConnection.Close();             }
            return dataReader;
        }

        public void Update(T val)
        {
            throw new NotImplementedException();
        }

        public void Delete(T val)
        {
            throw new NotImplementedException();
        }

        public void Insert(T val)
        {
            throw new NotImplementedException();
        }
    }
}
