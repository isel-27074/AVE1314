using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlMapper_v2
{
    public interface IConnectionPolicy
    {
        string dataSource { get; set; } //local instance
        string initialCatalog { get; set; } //database
        string integratedSecurity { get; set; } //default true 
        string connectionTimeout { get; set; } //default 15
        string pooling { get; set; } //true or false
     //   string[] entities { get; set; } //table names
        /* só actuamos sobre uma tabela de cada vez, quando
         * actualizamos uma tabela que tenha uma FK temos de
         * ver a anotação desse campo ou propriedade e obter
         * a informação de qual a tabela/classe à qual pertence,
         * para atualizar essa tabela/classe em cascata
        */
    }
}
