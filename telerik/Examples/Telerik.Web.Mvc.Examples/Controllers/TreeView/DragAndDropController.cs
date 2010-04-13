namespace Telerik.Web.Mvc.Examples
{
    using System.Web.Mvc;

    public partial class TreeViewController : Controller
    {
        public ActionResult DragAndDrop()
        {
            return View(GetRootEmployees());
        }
    }
}