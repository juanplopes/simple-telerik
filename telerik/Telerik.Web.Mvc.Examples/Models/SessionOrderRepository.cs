namespace Telerik.Web.Mvc.Examples.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;

    public static class SessionOrderRepository
    {
        public static IEnumerable<EditableOrder> All()
        {
            IEnumerable<EditableOrder> result = HttpContext.Current.Session["orders"] as IEnumerable<EditableOrder>;

            if (result == null)
            {
                HttpContext.Current.Session["orders"] = result = new NorthwindDataContext().Orders.Select(o => new EditableOrder { OrderID = o.OrderID, OrderDate = o.OrderDate ?? DateTime.Now, Employee = o.Employee, Freight = o.Freight ?? 0 }).ToList();
            }

            return result;
        }

        public static EditableOrder One(Func<EditableOrder, bool> predicate)
        {
            return All().Where(predicate).FirstOrDefault();
        }

        public static void Update(EditableOrder order)
        {
            EditableOrder editable = One(o => o.OrderID == order.OrderID);

            if (editable != null)
            {
                editable.OrderDate = order.OrderDate;
                editable.Employee = order.Employee;
                editable.Freight = order.Freight;
            }
        }
    }
}