namespace Telerik.Web.Mvc.Examples
{
    using System.Web.Mvc;
    using Telerik.Web.Mvc.Examples.Models;

    public partial class GridController : Controller
    {
        public ActionResult Paging(bool? pageInput, bool? nextPrevious, bool? numeric)
        {
            ViewData["pageInput"] = pageInput ?? false;
            ViewData["nextPrevious"] = nextPrevious ?? true;
            ViewData["numeric"] = numeric ?? true;

            return View(GetOrders());
        }

        [GridAction]
        public ActionResult _Paging()
        {
            return View(new GridModel<Order>
            {
                Data = GetOrders()
            });
        }
    }
}