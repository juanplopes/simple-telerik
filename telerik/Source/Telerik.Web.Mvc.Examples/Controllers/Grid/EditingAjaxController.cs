namespace Telerik.Web.Mvc.Examples
{
    using System.Linq;
    using System.Web.Mvc;
    
    using Models;
    using System;

    public partial class GridController : Controller
    {
        [SourceCodeFile("EditableCustomer (model)", "~/Models/EditableCustomer.cs")]
        [SourceCodeFile("SessionCustomerRepository", "~/Models/SessionCustomerRepository.cs")]
        [SourceCodeFile("Date.ascx (editor)", "~/Views/Shared/EditorTemplates/Date.ascx")]
        public ActionResult EditingAjax()
        {
            return View();
        }

        [GridAction]
        public ActionResult _SelectAjaxEditing()
        {
            return View(new GridModel(SessionCustomerRepository.All()));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [GridAction]
        public ActionResult _SaveAjaxEditing(string id)
        {
            EditableCustomer customer = SessionCustomerRepository.One(c => c.CustomerID == id);
            
            TryUpdateModel(customer);

            SessionCustomerRepository.Update(customer);

            return View(new GridModel(SessionCustomerRepository.All()));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [GridAction]
        public ActionResult _InsertAjaxEditing()
        {
            //Create a new instance of the EditableCustomer class.
            EditableCustomer customer = new EditableCustomer();

            //Perform model binding (fill the customer properties and validate it).
            if (TryUpdateModel(customer))
            {
                //The model is valid - insert the customer.

                SessionCustomerRepository.Insert(customer);
            }

            //Rebind the grid
            return View(new GridModel(SessionCustomerRepository.All()));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [GridAction]
        public ActionResult _DeleteAjaxEditing(string id)
        {
            //Find a customer with CustomerID equal to the id action parameter
            EditableCustomer customer = SessionCustomerRepository.One(c => c.CustomerID == id);

            if (customer != null)
            {
                //Delete the record
                SessionCustomerRepository.Delete(customer);
            }
            
            //Rebind the grid
            return View(new GridModel(SessionCustomerRepository.All()));
        }
    }
}
