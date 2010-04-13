namespace Telerik.Web.Mvc.Examples
{
    using System.Web.Mvc;
    using Telerik.Web.Mvc.Examples.Models;

    public partial class GridController : Controller
    {
        public ActionResult Sorting(bool? orderId, bool? contactName, bool? shipCountry, bool? orderDate)
        {
            ViewData["orderId"] = orderId ?? true;
            ViewData["contactName"] = contactName ?? false;
            ViewData["shipCountry"] = shipCountry ?? false;
            ViewData["orderDate"] = orderDate ?? false;

            return View(GetOrders());
        }

        [GridAction]
        public ActionResult _Sorting()
        {
            return View(new GridModel<Order>
            {
                Data = GetOrders()
            });
        }
    }
}