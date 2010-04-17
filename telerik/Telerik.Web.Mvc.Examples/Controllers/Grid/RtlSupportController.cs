namespace Telerik.Web.Mvc.Examples
{
    using System.Web.Mvc;
    using Telerik.Web.Mvc.Examples.Models;

    public partial class GridController : Controller
    {
        public ActionResult RtlSupport()
        {
            return View(GetOrders());
        }

        [GridAction]
        public ActionResult _RtlSupport()
        {
            return View(new GridModel<Order>
            {
                Data = GetOrders()
            });
        }
    }
}