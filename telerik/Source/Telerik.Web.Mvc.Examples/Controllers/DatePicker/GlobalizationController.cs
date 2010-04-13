namespace Telerik.Web.Mvc.Examples
{
    using System.Web.Mvc;

    public partial class DatePickerController : Controller
    {
        [CultureAwareAction]
        public ActionResult Globalization()
        {
            return View();
        }
    }
}