using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataModel
{
    [Table("Orders")]
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

        public Orders() { }

        public Orders(int CustomerID, int EmployeeID, string OrderDAte, string RequiredDate, string ShippedDate, string ShipVia, string Freight, string ShipName, string ShipAddress, string ShipCity, string ShipRegion, string ShipPostalCode, string ShipCountry)
            : this(0,CustomerID, EmployeeID, OrderDAte, RequiredDate, ShippedDate, ShipVia,Freight,ShipName,ShipAddress,ShipCity,ShipRegion,ShipPostalCode,ShipCountry)
        {
        }

        public Orders(int OrderID, int CustomerID, int EmployeeID, string OrderDAte, string RequiredDate, string ShippedDate, string ShipVia, string Freight, string ShipName, string ShipAddress, string ShipCity, string ShipRegion, string ShipPostalCode, string ShipCountry)
        {
			this.OrderID = OrderID;
			this.CustomerID = CustomerID;
			this.EmployeeID = EmployeeID;
			this.OrderDAte = OrderDAte;
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
            return OrderID + " - " + CustomerID + " - " + EmployeeID + " - " + OrderDAte + " - " + RequiredDate + " - " + ShippedDate + " - " + ShipVia + " - "+ Freight + " - " + ShipName + " - " + ShipAddress + " - " + ShippedDate + " - " + ShipCity + " - " + ShipRegion + "-" + ShipPostalCode + "-" + ShipCountry;
        }



    }
}
