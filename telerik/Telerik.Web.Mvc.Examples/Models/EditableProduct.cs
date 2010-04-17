namespace Telerik.Web.Mvc.Examples.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class EditableProduct
    {
        public int ProductID
        {
            get;
            set;
        }

        public string ProductName
        {
            get;
            set;
        }

        [DataType("Integer")]
        public int UnitsInStock
        {
            get;
            set;
        }

        [DataType(DataType.Currency)]
        public decimal UnitPrice
        {
            get;
            set;
        }

        [DataType(DataType.Date)]
        public DateTime LastSupply
        {
            get;
            set;
        }
    }
}