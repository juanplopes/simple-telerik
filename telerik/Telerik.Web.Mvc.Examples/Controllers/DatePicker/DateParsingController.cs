namespace Telerik.Web.Mvc.Examples
{
    using System;
    using System.Web.Mvc;
    using System.Threading;

    public partial class DatePickerController : Controller
    {
        public ActionResult DateParsing()
        {
            ViewData["dateFormat"] = Thread.CurrentThread.CurrentCulture.DateTimeFormat.ShortDatePattern;
            return View();
        }
    }
}