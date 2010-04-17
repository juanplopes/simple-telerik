namespace Telerik.Web.Mvc.Examples
{
    using System.Web.Mvc;
    using Telerik.Web.Mvc.Examples.Models;

    public partial class PanelBarController : Controller
    {
        public ActionResult DataBindingToModel()
        {
            NorthwindDataContext northwind = new NorthwindDataContext();
            return View(northwind.Categories);
        }
    }
}