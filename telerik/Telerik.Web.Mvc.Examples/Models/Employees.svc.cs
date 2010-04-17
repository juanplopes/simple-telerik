namespace Telerik.Web.Mvc.Examples.Models
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.ServiceModel;
    using System.ServiceModel.Activation;
    using Telerik.Web.Mvc.UI;

    [ServiceContract(Namespace = "")]
    [ServiceBehavior(IncludeExceptionDetailInFaults = true)]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class EmployeesWcf
    {
        [OperationContract]
        public IEnumerable<TreeViewItemModel> GetEmployees(TreeViewItemModel node)
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

            return nodes.Cast<TreeViewItemModel>();
        }
    }

    public class SampleModel
    {
        public IEnumerable Data { get; set; }
    }
}
