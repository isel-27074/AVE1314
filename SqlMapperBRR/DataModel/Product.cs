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

        public Product() { }

        public Product(string ProductName, string QuantityPerUnit, decimal UnitPrice, short UnitsInStock, short UnitsOnOrder)
            : this (0, ProductName, QuantityPerUnit, UnitPrice, UnitsInStock, UnitsOnOrder)
        {
        }

        public Product(int ProductID, string ProductName, string QuantityPerUnit, decimal UnitPrice, short UnitsInStock, short UnitsOnOrder) {
            this.ProductID = ProductID;
            this.ProductName = ProductName;
            this.QuantityPerUnit = QuantityPerUnit;
            this.UnitPrice = UnitPrice;
            this.UnitsInStock = UnitsInStock;
            this.UnitsOnOrder = UnitsOnOrder;
        }

        public override string ToString()
        {
            return ProductID + " - " + ProductName + " - " + QuantityPerUnit + " - " + UnitPrice + " - " + UnitsInStock + " - " + UnitsOnOrder;
        }

    }
}
