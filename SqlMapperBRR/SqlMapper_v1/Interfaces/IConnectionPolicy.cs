using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlMapper_v1
{
    public interface IConnectionPolicy
    {
        string dataSource { get; set; } //local instance
        string initialCatalog { get; set; } //database
        string integratedSecurity { get; set; } //default true 
        string connectionTimeout { get; set; } //default 15
        string pooling { get; set; } //true or false
        //string[] entities { get; set; } //table names
    }
}
