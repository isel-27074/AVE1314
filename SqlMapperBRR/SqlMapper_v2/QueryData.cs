using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlMapper_v2
{
    public class QueryData : IQueryData
    {
        public string[] columns { get; set; }
        
        public Dictionary<string, string[]> parTabelaColunas { get; set; }

        public QueryData(Dictionary<string, string[]> ptc)
        {
            parTabelaColunas = ptc;
        }

        public IEnumerable<string> GetTables()
        {
            return parTabelaColunas.Keys.Distinct();
        }

    }
}
