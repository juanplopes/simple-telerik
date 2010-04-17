namespace Telerik.Web.Mvc.Examples
{
    using System.Web.Mvc;

    public partial class GridController : Controller
    {
        public ActionResult TemplatesClientSide()
        {
            return View();
        }

        [GridAction]
        public ActionResult _TemplatesClientSide()
        {
            return View(new GridModel(GetCustomers()));
        }
    }
}