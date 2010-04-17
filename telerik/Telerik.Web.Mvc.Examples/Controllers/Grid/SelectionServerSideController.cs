namespace Telerik.Web.Mvc.Examples
{
    using System.Web.Mvc;

    public partial class GridController : Controller
    {
        public ActionResult SelectionServerSide(string id)
        {
            ViewData["Customers"] = GetCustomers();
            if (string.IsNullOrEmpty(id))
            {
                id = "ALFKI";
            }
            ViewData["id"] = id;
            ViewData["Orders"] = GetOrdersForCustomer(id);
            return View();
        }
    }
}
