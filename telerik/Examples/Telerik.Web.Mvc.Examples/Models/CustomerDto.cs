namespace Telerik.Web.Mvc.Examples.Models
{
    using System.Runtime.Serialization;
    
    [KnownType(typeof(CustomerDto))]
    public class CustomerDto
    {
        public string ContactName
        {
            get;
            set;
        }

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

        public string Country
        {
            get;
            set;
        }
    }
}
