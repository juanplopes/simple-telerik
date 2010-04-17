namespace Telerik.Web.Mvc.Examples
{
    using System;
    using System.Web.Mvc;

    public partial class CalendarController : Controller
    {
        public ActionResult SelectAction(DateTime? date)
        {
            ViewData["date"] = date;
            return View();
        }

        public ActionResult ReturnContent(string sMsg)
        {
            return Content(Server.HtmlEncode(sMsg));
        }
    }
}