using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataModel
{
    [Table("Customers")]
    class Customers
    {
        [Key]
        public int CustomerID { set; get; }
        public string CompanyName { set; get; }
        public string ContactName { set; get; }
        public string ContactTitle { set; get; }
        public string Address { set; get; }
        public string City { set; get; }
        public string Region { set; get; }
        public string PostalCode { set; get; }
        public string Country { set; get; }
        public int Phone { set; get; }
    }
}
