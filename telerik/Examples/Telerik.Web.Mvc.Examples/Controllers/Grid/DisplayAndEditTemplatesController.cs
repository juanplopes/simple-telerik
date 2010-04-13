namespace Telerik.Web.Mvc.Examples
{
    using System;
    using System.Linq;
    using System.Web.Mvc;

    using Extensions;

    using Models;

    public partial class GridController
    {
        [SourceCodeFile("EditableOrder (model)", "~/Models/EditableOrder.cs")]
        [SourceCodeFile("Employee (model)", "~/Models/Employee.cs")]
        [SourceCodeFile("Employee.ascx (Display)", "~/Views/Grid/DisplayTemplates/Employee.ascx")]
        [SourceCodeFile("Employee.ascx (Editor)", "~/Views/Grid/EditorTemplates/Employee.ascx")]
        public ActionResult DisplayAndEditTemplates()
        {
            PopulateEmployees();

            return View(SessionOrderRepository.All());
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult UpodateOrder(int id, int employee)
        {
            EditableOrder order = new EditableOrder
                                      {
                                          OrderID = id,
                                          Employee = new NorthwindDataContext().Employees.SingleOrDefault(e => e.EmployeeID == employee)
                                      };

            // Exclude "Employee" from the list of updated properties
            if (TryUpdateModel(order, null, null, new [] { "Employee" }))
            {
                SessionOrderRepository.Update(order);

                return RedirectToAction("DisplayAndEditTemplates", this.GridRouteValues());
            }

            PopulateEmployees();

            return View("DisplayAndEditTemplates", SessionOrderRepository.All());
        }

        private void PopulateEmployees()
        {
            ViewData["employees"] = new NorthwindDataContext().Employees
                                                              .Select(e => new { Id = e.EmployeeID, Name = e.FirstName + " " + e.LastName })
                                                              .OrderBy(e => e.Name);
        }
    }
}