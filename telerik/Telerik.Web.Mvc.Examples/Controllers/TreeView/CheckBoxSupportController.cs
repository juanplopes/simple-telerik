namespace Telerik.Web.Mvc.Examples
{
    using System.Web.Mvc;
    using System.Linq;
    using Models;
    
    using System.Collections;
    using System.Collections.Generic;

    using Telerik.Web.Mvc.UI;

    public partial class TreeViewController : Controller
    {
        public ActionResult CheckBoxSupport()
        {
            return View(GetRootEmployees());
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult CheckBoxSupport(List<TreeViewItemModel> checkedNodes)
        {
            string message = string.Empty;

            if (checkedNodes != null)
            {
                foreach (TreeViewItemModel node in checkedNodes)
                {
                    message += node.Text + "<br/>";
                }
            }

            ViewData["message"] = message;
            ViewData["checkedNodes"] = checkedNodes;
            return View(GetRootEmployees());
        }
    }
}