namespace Telerik.Web.Mvc.JavaScriptTest
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel;
    
    public class Customer
    {
        [Required]
        public string Name { get; set; }
        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }
        public bool Active { get; set; }

        [ReadOnly(true)]
        public int ReadOnly
        {
            get;
            set;
        }
        public Gender Gender
        {
            get;
            set;
        }
    }

    public enum Gender
    {
        Female,
        Male
    }
}
