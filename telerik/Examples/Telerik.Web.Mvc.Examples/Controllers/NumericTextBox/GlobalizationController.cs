namespace Telerik.Web.Mvc.Examples
{
    using System.Web.Mvc;

    public partial class NumericTextBoxController : Controller
    {
        [CultureAwareAction]
        public ActionResult Globalization()
        {
            return View();
        }
    }
}