using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;

namespace Telerik.Web.Mvc.JavaScriptTest.Controllers
{
    public class DatePickerController : Controller
    {
        //
        // GET: /DatePicker/

        public ActionResult DateParsing()
        {
            return View();
        }

        public ActionResult DatePicker() 
        {
            return View();
        }

        public ActionResult ParseByToken() 
        {
            return View();
        }

        public ActionResult DatePickerClientAPI() 
        {
            return View();
        }
    }
}
