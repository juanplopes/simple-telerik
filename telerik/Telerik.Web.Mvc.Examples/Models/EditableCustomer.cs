namespace Telerik.Web.Mvc.Examples.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using System.Runtime.Serialization;

    [KnownType(typeof(EditableCustomer))]
    public class EditableCustomer
    {
        [Required]
        public string ContactName
        {
            get;
            set;
        }

        [Required]
        public string Address
        {
            get;
            set;
        }

        public string CustomerID
        {
            get;
            set;
        }

        [Required]
        public string Country
        {
            get;
            set;
        }

        [DataType(DataType.Date)]
        public DateTime BirthDay
        {
            get;
            set;
        }
    }
}