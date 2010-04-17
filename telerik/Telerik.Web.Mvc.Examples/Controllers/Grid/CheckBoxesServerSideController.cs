namespace Telerik.Web.Mvc.Examples
{
    using System.Web.Mvc;
    using System.Linq;
    using Models;

    public partial class GridController : Controller
    {
        public ActionResult CheckBoxesServerSide(int[] checkedRecords)
        {
            checkedRecords  = checkedRecords ?? new int[] { };
            ViewData["checkedRecords"] = checkedRecords;
            
            if (checkedRecords.Any())
            {
                ViewData["checkedOrders"] = GetOrders().Where(o => checkedRecords.Contains(o.OrderID));
            }

            return View(GetOrders());
        }
    }
}