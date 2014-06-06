using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlMapperBRR
{
    public interface IDataMapper<T>
    {
        IEnumerable<T> GetAll();
        void Update(T val);
        void Delete(T val);
        void Insert(T val);
    }
}
