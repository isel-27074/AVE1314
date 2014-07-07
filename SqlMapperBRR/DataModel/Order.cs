using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SqlMapper_v3;

namespace DataModel
{
    [Table("Orders")]
    public class Order
    {
        [Key]
        public int OrderID { set; get; }
        [ForeignKey("Customers")]
        //[ForeignKeyMapper("Customers", typeof(Customer))]
        //public string CustomerID { set; get; }
        public Customer CustomerID;// { set; get; }
        [ForeignKey("Employees")]
        //[ForeignKeyMapper("Employees", typeof(Employee))]
        //public int EmployeeID { set; get; }
        public Employee EmployeeID { set; get; }
        public DateTime OrderDate { set; get; }
        public DateTime RequiredDate { set; get; }
        public DateTime ShippedDate { set; get; }
        public int ShipVia { set; get; }
        // Money converts into decimal.
        // Mapping CLR Parameter Data:
        // http://msdn.microsoft.com/en-us/library/ms131092.aspx
        public decimal Freight { set; get; }
        public string ShipName { set; get; }
        public string ShipAddress { set; get; }
        public string ShipCity { set; get; }
        public string ShipRegion { set; get; }
        public string ShipPostalCode { set; get; }
        public string ShipCountry { set; get; }

        public Order() { }

        public Order(Customer CustomerID, Employee EmployeeID, DateTime OrderDate, DateTime RequiredDate, DateTime ShippedDate,
            int ShipVia, decimal Freight, string ShipName, string ShipAddress, string ShipCity, string ShipRegion,
            string ShipPostalCode, string ShipCountry)
            : this(0, CustomerID, EmployeeID, OrderDate, RequiredDate, ShippedDate, ShipVia, Freight, ShipName, ShipAddress,
            ShipCity, ShipRegion, ShipPostalCode, ShipCountry)
        {
        }

        public Order(int OrderID, Customer CustomerID, Employee EmployeeID, DateTime OrderDate, DateTime RequiredDate,
            DateTime ShippedDate, int ShipVia, decimal Freight, string ShipName, string ShipAddress, string ShipCity,
            string ShipRegion, string ShipPostalCode, string ShipCountry)
        {
			this.OrderID = OrderID;
			this.CustomerID = CustomerID;
			this.EmployeeID = EmployeeID;
			this.OrderDate = OrderDate;
			this.RequiredDate = RequiredDate;
			this.ShippedDate = ShippedDate;
			this.ShipVia = ShipVia;
			this.Freight = Freight;
			this.ShipName = ShipName;
			this.ShipAddress = ShipAddress;
			this.ShipCity = ShipCity;
			this.ShipRegion = ShipRegion;
			this.ShipPostalCode = ShipPostalCode; 
			this.ShipCountry = ShipCountry;
        }

        public override string ToString()
        {
            return OrderID + "-" + CustomerID + "-" + EmployeeID + "-" + OrderDate + "-" + RequiredDate + "-" +
                ShippedDate + "-" + ShipVia + "-"+ Freight + "-" + ShipName + "-" + ShipAddress + "-" +
                ShipCity + "-" + ShipRegion + "-" + ShipPostalCode + "-" + ShipCountry;
        }
    }
}
