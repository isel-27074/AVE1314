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
        public string region { set; get; }
        public string PostalCode { set; get; }
        
    }
}
