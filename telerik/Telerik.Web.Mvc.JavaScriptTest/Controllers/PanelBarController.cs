using System.Web.Mvc;

namespace Telerik.Web.Mvc.JavaScriptTest.Controllers
{
    public class PanelBarController : Controller
    {
        public ActionResult ExpandCollapse()
        {
            return View();
        }

        public ActionResult SingleExpandCollapse() 
        {
            return View();
        }

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

        public ActionResult SingleExpandClientAPI() 
        {
            return View();
        }
    }
}
