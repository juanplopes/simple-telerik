namespace Telerik.Web.Mvc.Examples
{
    using System.Web.Mvc;
    
    using Models;

    public partial class GridController : Controller
    {
        public ActionResult SelectionClientSide(string id)
        {
            ViewData["Customers"] = GetCustomers();
            ViewData["Orders"] = GetOrdersForCustomer(id ?? "ALFKI");
            ViewData["id"] = "ALFKI";
            return View();
        }

        [GridAction]
        public ActionResult _SelectionClientSide_Orders(string customerID)
        {
            customerID = customerID ?? "ALFKI";

            return View(new GridModel<Order>
            {
                Data = GetOrdersForCustomer(customerID)
            });
        }

        [GridAction]
        public ActionResult _SelectionClientSide_Customers()
        {
            return View(new GridModel<Customer>
            {
                Data = GetCustomers()
            });
        }
    }
}
