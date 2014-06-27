using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataModel
{
    [Table("Employees")]
    class Orders
    {
        [Key]
        public int OrderID { set; get; }

        [ForeignKey("Customer")]
        public int CustomerID { set; get; }
        [ForeignKey("Employees")]
        public int EmployeeID { set; get; }
        public string OrderDAte { set; get; }
        public string RequiredDate { set; get; }
        public string ShippedDate { set; get; }
        public string ShipVia { set; get; }
        public string Freight { set; get; }
        public string ShipName { set; get; }
        public string ShipAddress { set; get; }
        public string ShipCity { set; get; }
        public string ShipRegion { set; get; }
        public string ShipPostalCode { set; get; }
        public string ShipCountry { set; get; }



    }
}
