using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataModel
{
    [Table("Products")]
    public class Product
    {
        [Key]
        public int ProductID { set; get; } 
        public string ProductName { set; get; } 
        public string QuantityPerUnit { set; get; } 
        public decimal UnitPrice { set; get; } 
        public short UnitsInStock { set; get; } 
        public short UnitsOnOrder { set; get; }
    }
}
