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
    public class Customer
    {
        [Key]
        public string CustomerID { set; get; }
        public string CompanyName { set; get; }
        public string ContactName { set; get; }
        public string ContactTitle { set; get; }
        public string Address { set; get; }
        public string City { set; get; }
        public string Region { set; get; }
        public string PostalCode { set; get; }
        public string Country { set; get; }
        public string Phone { set; get; }
        public string Fax { set; get; }

        public Customer() { }

        public Customer(string CompanyName, string ContactName, string ContactTitle, string Address, string City,
            string Region, string PostalCode, string Country, string Phone, string Fax)
            : this("C0000", CompanyName, ContactName, ContactTitle, Address, City, Region, PostalCode, Country, Phone, Fax)
        {
        }

        public Customer(string CustomerID, string CompanyName, string ContactName, string ContactTitle, string Address,
            string City, string Region, string PostalCode, string Country, string Phone, string Fax)
        {
            this.CustomerID = CustomerID;
            this.CompanyName = CompanyName;
            this.ContactName = ContactName;
            this.ContactTitle = ContactTitle;
            this.Address = Address;
            this.City = City;
            this.Region = Region;
            this.PostalCode = PostalCode;
            this.Country = Country;
            this.Phone = Phone;
            this.Fax = Fax;
        }

        public override string ToString()
        {
            return CustomerID + " - " + CompanyName + " - " + ContactName + " - " + ContactTitle + " - " + Address + " - " +
                City + " - " + Region + " - " + PostalCode + " - " + Country + " - " + Phone + " - " + Fax;
        }
    }
}
