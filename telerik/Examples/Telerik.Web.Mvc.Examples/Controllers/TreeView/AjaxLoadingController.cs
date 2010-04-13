namespace Telerik.Web.Mvc.Examples
{
    using System;
    using System.Collections;
    using System.Linq;
    using System.Web.Mvc;
    using Models;
    using Telerik.Web.Mvc.UI;

    public partial class TreeViewController : Controller
    {
        public ActionResult AjaxLoading()
        {
            return View(GetRootEmployees());
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult _AjaxLoading(TreeViewItemModel node)
        {
            NorthwindDataContext northwind = new NorthwindDataContext();

            int? parentId = !string.IsNullOrEmpty(node.Value) ? (int?) Convert.ToInt32(node.Value)  : null;
            
            IEnumerable nodes = from item in northwind.Employees
                                where item.ReportsTo == parentId || (parentId == null && item.ReportsTo == null)
                                select new TreeViewItemModel
                                {
                                    Text = item.FirstName + " " + item.LastName,
                                    Value = item.EmployeeID.ToString(),
                                    LoadOnDemand = item.Employees.Count > 0,
                                    Enabled = true
                                };

            return new JsonResult { Data = nodes };
        }
    }
}