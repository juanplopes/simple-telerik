namespace Telerik.Web.Mvc.Examples
{
    using System.Web.Mvc;

    public partial class GridController : Controller
    {
        public ActionResult Grouping(bool? orderDate, bool? shipAddress, bool? contactName)
        {
            ViewData["orderDate"] = orderDate ?? false;
            ViewData["shipAddress"] = shipAddress ?? false;
            ViewData["contactName"] = contactName ?? true;
            
            return View(GetOrders());
        }

        [GridAction]
        public ActionResult GroupingAjax()
        {
            return View(new GridModel(GetOrders()));
        }
    }
}