namespace Telerik.Web.Mvc.Examples
{
    using System;
    using System.Web.Mvc;

    public partial class CalendarController : Controller
    {
        public ActionResult FirstLook(DateTime? selectedDate, DateTime? minDate, DateTime? maxDate)
        {
            ViewData["selectedDate"] = selectedDate ?? DateTime.Today;
            ViewData["minDate"] = minDate ?? new DateTime(1900, 1, 1);
            ViewData["maxDate"] = maxDate ?? new DateTime(2099, 12, 31);

            return View();
        }
    }
}