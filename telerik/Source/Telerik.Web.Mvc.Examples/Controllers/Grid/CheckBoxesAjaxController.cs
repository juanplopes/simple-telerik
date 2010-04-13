namespace Telerik.Web.Mvc.Examples
{
    using System.Web.Mvc;
    using System.Linq;
    using Models;

    public partial class GridController : Controller
    {
        [SourceCodeFile("CheckedOrders.ascx", "~/Views/Grid/CheckedOrders.ascx")]
        public ActionResult CheckBoxesAjax()
        {
            return View();
        }

        [GridAction]
        public ActionResult _CheckBoxesAjax()
        {
            return View(new GridModel(GetOrders()));
        }

        public ActionResult DisplayCheckedOrders(int[] checkedRecords)
        {
            checkedRecords = checkedRecords ?? new int[]{};
            return PartialView("CheckedOrders", GetOrders().Where(o => checkedRecords.Contains(o.OrderID)));
        }
    }
}