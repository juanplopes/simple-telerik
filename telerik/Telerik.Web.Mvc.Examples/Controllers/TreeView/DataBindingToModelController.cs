namespace Telerik.Web.Mvc.Examples
{
    using System.Web.Mvc;
    using Telerik.Web.Mvc.Examples.Models;

    public partial class TreeViewController : Controller
    {
        public ActionResult DataBindingToModel()
        {
            return View(GetCategories());
        }
    }
}