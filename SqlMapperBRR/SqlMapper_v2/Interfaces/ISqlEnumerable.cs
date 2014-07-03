using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlMapper_v2
{
    public interface ISqlEnumerable<T> : IEnumerable<T>
    {
        ISqlEnumerable<T> where(string clause);

    }
}
