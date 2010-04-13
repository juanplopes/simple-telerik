namespace Telerik.Web.Mvc.Examples
{
    using System.Web.Mvc;
    using Models;

    public partial class GridController : Controller
    {
        [CultureAwareAction]
        public ActionResult Localization()
        {
            return View();
        }
    }
}