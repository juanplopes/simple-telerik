namespace Telerik.Web.Mvc.Examples
{
    using System;
    using System.Web.Mvc;
    using Telerik.Web.Mvc.Examples.Models;

    public partial class GridController : Controller
    {
        public ActionResult FirstLook(bool? ajax, bool? scrolling, bool? paging, bool? filtering, bool? sorting, bool? grouping, bool? showFooter)
        {
            ViewData["ajax"] = ajax ?? true;
            ViewData["scrolling"] = scrolling ?? true;
            ViewData["paging"] = paging ?? true;
            ViewData["filtering"] = filtering ?? true;
            ViewData["grouping"] = grouping ?? true;
            ViewData["sorting"] = sorting ?? true;
            ViewData["showFooter"] = showFooter ?? true;

            return View(GetOrderDto());
        }

        [GridAction]
        public ActionResult _FirstLook()
        {
            return View(new GridModel(GetOrderDto()));
        }
    }
}