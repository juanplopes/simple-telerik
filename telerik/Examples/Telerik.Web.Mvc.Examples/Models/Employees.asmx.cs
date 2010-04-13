namespace Telerik.Web.Mvc.Examples.Models
{
    using System;
    using System.Collections;
    using System.Linq;
    using System.Web.Script.Services;
    using System.Web.Services;
    using Telerik.Web.Mvc.UI;

    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    [ScriptService]
    public class Employees : System.Web.Services.WebService
    {
        [WebMethod]
        public IEnumerable GetEmployees(TreeViewItemModel node)
        {
            NorthwindDataContext northwind = new NorthwindDataContext();

            int? parentId = !string.IsNullOrEmpty(node.Value) ? (int?)Convert.ToInt32(node.Value) : null;

            IEnumerable nodes = from item in northwind.Employees
                                where item.ReportsTo == parentId || (parentId == null && item.ReportsTo == null)
                                select new TreeViewItemModel
                                {
                                    Text = item.FirstName + " " + item.LastName,
                                    Value = item.EmployeeID.ToString(),
                                    LoadOnDemand = item.Employees.Count > 0
                                };
            
          return nodes;
        }
    }
}
