using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlMapper_v1
{
    public interface IQueryData
    {
        string[] columns { get; set; } //column names
        Dictionary<string, string[]> parTabelaColunas { get; set; } //par tabela coluna
    }
}
