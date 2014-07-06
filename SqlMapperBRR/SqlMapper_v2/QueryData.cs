using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlMapper_v2
{
    public class QueryData : IQueryData
    {
        public Dictionary<string, string[]> _tableColumnPair { get; set; }

        public QueryData(Dictionary<string, string[]> tcp)
        {
            _tableColumnPair = tcp;
        }

        public Dictionary<string, string[]> GetQueryData()
        {
            return _tableColumnPair;
        }
    }
}
