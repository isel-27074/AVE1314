using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlMapper_v1
{
    public interface IQueryData
    {
        Dictionary<string, string[]> _tableColumnPair { get; set; } //par tabela coluna
        Dictionary<string, string[]> GetQueryData();
    }
}
