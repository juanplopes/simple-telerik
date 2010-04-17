namespace Telerik.Web.Mvc.Examples
{
    using System.Web.Mvc;

    using Models;

    public partial class GridController : Controller
    {
        public ActionResult ClientSideEvents()
        {
            return View();
        }

        [GridAction]
        public ActionResult _ClientSideEvents_Ajax()
        {
            return View(new GridModel<Order>
            {
                Data = GetOrders()
            });
        }
    }
}
