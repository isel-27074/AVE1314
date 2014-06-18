using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlMapper_v1
{
    public class QueryData : IQueryData
    {
        public Dictionary<string, string> parTabelaColuna { get; set; }

        public QueryData(Dictionary<string, string> ptc)
        {
            parTabelaColuna = ptc;
        }

        public IEnumerable<string> GetTables()
        {
            return parTabelaColuna.Keys.Distinct();
        }

    }
}
