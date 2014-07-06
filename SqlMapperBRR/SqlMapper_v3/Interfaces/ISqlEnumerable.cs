using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlMapper_v3
{
    public interface ISqlEnumerable<T> : IEnumerable<T>, ISqlEnumerable
    {
        ISqlEnumerable<T> Where(string clause);
    }

    public interface ISqlEnumerable : IEnumerable
    {
        ISqlEnumerable Where(string clause);
    }
}
