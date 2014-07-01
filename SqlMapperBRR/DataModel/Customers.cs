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

        public Customers() { }

        public Customers(string CompanyName, string ContactName, string ContactTitle, string Address, string City, string Region, string PostalCode, string Country, int Phone)
            : this(0, CompanyName, ContactName, ContactTitle, Address, City, Region, PostalCode, Country, Phone)
        {
        }

        public Customers(int CustomerID, string CompanyName, string ContactName, string ContactTitle, string Address, string City, string Region, string PostalCode, string Country, int Phone)
        {
            this.CustomerID = CustomerID;
            this.CompanyName = CompanyName;
            this.ContactName = ContactTitle;
            this.Address = Address;
            this.City = City;
            this.Region = Region;
            this.PostalCode = PostalCode;
            this.Country = Country;
            this.Phone = Phone;
        }

        public override string ToString()
        {
            return CustomerID + " - " + CompanyName + " - " + ContactName + " - " + Address + " - " + City + " - " + Region + "-" + PostalCode + "-" + Country + "-" + Phone;
        }
    }
}
