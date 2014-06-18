using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace SqlMapper_v1
{
    public class DataMapper<T> : IDataMapper<T> where T : class
    {
        private SqlDataReader _dr;

        public DataMapper(SqlDataReader dr)
        {
            _dr = dr;
        }
        
        public IEnumerable<T> GetAll()
        {
            throw new NotImplementedException();
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
