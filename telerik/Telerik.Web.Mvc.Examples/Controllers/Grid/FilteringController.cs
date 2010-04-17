namespace Telerik.Web.Mvc.Examples
{
    using System.Web.Mvc;
    using Telerik.Web.Mvc.Examples.Models;

    public partial class GridController : Controller
    {
        public ActionResult Filtering(string startsWith)
        {
            ViewData["startsWith"] = startsWith ?? "Paul";
            return View(GetOrders());
        }

        [GridAction]
        public ActionResult _Filtering()
        {
            return View(new GridModel<Order>
            {
                Data = GetOrders()
            });
        }
    }
}