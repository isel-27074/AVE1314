using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlMapper_v3
{
    public class ConnectionPolicy: IConnectionPolicy
    {
        public string dataSource { get; set; }

        public string initialCatalog { get; set; }

        public string integratedSecurity { get; set; }

        public string connectionTimeout { get; set; }

        public string pooling { get; set; }

        public ConnectionPolicy(string ds, string icat, string isec, string ctime, string pooling) {
            dataSource = ds;
            initialCatalog = icat;
            integratedSecurity = isec;
            connectionTimeout = ctime;
            this.pooling = pooling;            
        }

    }
}
