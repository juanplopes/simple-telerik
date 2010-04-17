namespace Telerik.Web.Mvc.Examples.Models
{
    using System;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    public class EditableOrder
    {
        [ReadOnly(true)]
        public int OrderID { get; set; }

        [UIHint("Employee"), Required]
        public Employee Employee { get; set; }

        [DataType(DataType.Date), Required]
        public DateTime OrderDate { get; set; }

        [DataType(DataType.Currency), Required]
        public decimal Freight { get; set; }
    }
}