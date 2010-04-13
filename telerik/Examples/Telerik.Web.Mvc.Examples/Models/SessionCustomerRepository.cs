namespace Telerik.Web.Mvc.Examples.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    
    public static class SessionCustomerRepository
    {
        public static IList<EditableCustomer> All()
        {
            IList<EditableCustomer> result = (IList<EditableCustomer>)HttpContext.Current.Session["Customers"];
            
            if (result == null)
            {
                HttpContext.Current.Session["Customers"] = result =
                    (from customer in new NorthwindDataContext().Customers
                     select new EditableCustomer
                     {
                         Address = customer.Address,
                         ContactName = customer.ContactName,
                         Country = customer.Country,
                         CustomerID = customer.CustomerID,
                         BirthDay = DateTime.Now
                     }).ToList();
            }

            return result;
        }

        public static EditableCustomer One(Func<EditableCustomer, bool> predicate)
        {
            return All().Where(predicate).FirstOrDefault();
        }

        public static void Insert(EditableCustomer customer)
        {
            customer.CustomerID = Guid.NewGuid().ToString();
            
            All().Insert(0, customer);
        }

        public static void Update(EditableCustomer customer)
        {
            EditableCustomer target = One(c => c.CustomerID == customer.CustomerID);
            if (target != null)
            {
                target.Address = customer.Address;
                target.ContactName = customer.ContactName;
                target.Country = customer.Country;
                target.BirthDay = customer.BirthDay;
            }
        }
        
        public static void Delete(EditableCustomer customer)
        {
            EditableCustomer target = One(c => c.CustomerID == customer.CustomerID);
            if (target != null)
            {
                All().Remove(target);
            }
        }
    }
}
