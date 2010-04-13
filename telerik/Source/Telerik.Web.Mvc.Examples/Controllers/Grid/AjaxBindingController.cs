namespace Telerik.Web.Mvc.Examples
{
    using System.Web.Mvc;
    using Models;

    public partial class GridController : Controller
    {
        public ActionResult AjaxBinding()
        {
            return View();
        }

        [GridAction]
        public ActionResult _AjaxBinding()
        {
            return View(new GridModel<Order>
            {
                Data = GetOrders()
            });
        }
    }
}