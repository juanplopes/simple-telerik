using System.Web.Mvc;

namespace Telerik.Web.Mvc.JavaScriptTest.Controllers
{
    public class TabStripController : Controller
    {
        public ActionResult AjaxLoadingTests()
        {
            return View();
        }

        public ActionResult AjaxView1()
        {
            return PartialView();
        }

        public ActionResult AjaxView2()
        {
            return PartialView();
        }

        public ActionResult ClientAPI() 
        {
            return View();
        }
    }
}
