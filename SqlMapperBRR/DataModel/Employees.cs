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
    class Employees
    {

        [Key]
        [ForeignKey("Employees")]
        public int EmplyeeID { set; get; }
        public string LastName { set; get; }
        public string FirstName { set; get; }
        public string Title { set; get; }
        public string TitleOfCourtesy { set; get; }
        public string BirthDate { set; get; }
        public string HireDate { set; get; }
        public string Address { set; get; }
        public string City { set; get; }
        public string Region { set; get; }
        public string PostalCode { set; get; }

        public Employees() { }

        public Employees(string LastName, string FirstName, string Title, string TitleOfCourtesy, string BirthDate, string HireDate, string Address, string City, string Region, string PostalCode)
            : this(0, LastName, FirstName, Title, TitleOfCourtesy, BirthDate, HireDate, Address, City, Region, PostalCode)
        {
        }

        public Employees(int EmplyeeID, string LastName, string FirstName, string Title, string TitleOfCourtesy, string BirthDate, string HireDate, string Address, string City, string Region, string PostalCode)
        {
            this.EmplyeeID = EmplyeeID;
            this.LastName = LastName;
            this.FirstName = FirstName;
            this.Title = Title;
            this.TitleOfCourtesy = TitleOfCourtesy;
            this.BirthDate = BirthDate;
            this.HireDate = HireDate;
            this.Address = Address;
            this.City = City;
            this.Region = Region;
            this.PostalCode = PostalCode;
        }

        public override string ToString()
        {
            return EmplyeeID + " - " + LastName + " - " + FirstName + " - " + Title + " - " + TitleOfCourtesy + " - " + BirthDate + "-" + HireDate + "-" + Address + "-" + City + "-" + Region + "-" + PostalCode;
        }

    }
}
