using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlMapperBRR
{
    [@SQLTableName("Products")]
    class Product
    {
        public int ProductID { set; get; } 
        public string ProductName { set; get; } 
        public string QuantityPerUnit { set; get; } 
        public decimal UnitPrice { set; get; } 
        public short UnitsInStock { set; get; } 
        public short UnitsOnOrder { set; get; }
    }
}
