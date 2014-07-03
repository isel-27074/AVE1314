using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlMapper_v3
{
    public interface IDataMapper<T> : IDataMapper
    {
        new SqlEnumerable<T> GetAll();
        void Update(T val);
        void Delete(T val);
        void Insert(T val);
    }

    public interface IDataMapper
    {
        SqlEnumerable GetAll();
        void Update(object val);
        void Delete(object val);
        void Insert(object val);
    }
}
