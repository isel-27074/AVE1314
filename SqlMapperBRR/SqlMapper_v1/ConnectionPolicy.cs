using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlMapper_v1
{
    public class ConnectionPolicy: IConnectionPolicy
    {
        public string dataSource { get; set; }

        public string initialCatalog { get; set; }

        public string integratedSecuriry { get; set; }

        public string connectionTimeout { get; set; }

        public string pooling { get; set; }

    }
}
