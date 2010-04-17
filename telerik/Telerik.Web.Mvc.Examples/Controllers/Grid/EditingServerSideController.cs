namespace Telerik.Web.Mvc.Examples
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;
    using Telerik.Web.Mvc.Extensions;
    
    using Models;

    public partial class GridController : Controller
    {
        [SourceCodeFile("EditableCustomer (model)", "~/Models/EditableCustomer.cs")]
        [SourceCodeFile("Date.ascx (editor)", "~/Views/Shared/EditorTemplates/Date.ascx")]
        [SourceCodeFile("SessionCustomerRepository", "~/Models/SessionCustomerRepository.cs")]
        public ActionResult EditingServerSide()
        {
            return View(SessionCustomerRepository.All());
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Insert()
        {
            //Create a new instance of the EditableCustomer class.
            EditableCustomer customer = new EditableCustomer();

            //Perform model binding (fill the customer properties and validate it).
            if (TryUpdateModel(customer))
            {
                //The model is valid - insert the customer and redisplay the grid.
                SessionCustomerRepository.Insert(customer);

                //GridRouteValues() is an extension method which returns the 
                //route values defining the grid state - current page, sort expression, filter etc.
                return RedirectToAction("EditingServerSide", this.GridRouteValues());
            }

            //The model is invalid - render the current view to show any validation errors
            return View("EditingServerSide", SessionCustomerRepository.All());
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Save(string id)
        {
            //Create a new instance of the EditableCustomer class and set its CustomerID property.
            EditableCustomer customer = new EditableCustomer
            {
                CustomerID = id
            };

            //Perform model binding (fill the customer properties and validate it).
            if (TryUpdateModel(customer))
            {
                //The model is valid - update the customer and redisplay the grid.
                SessionCustomerRepository.Update(customer);

                //GridRouteValues() is an extension method which returns the 
                //route values defining the grid state - current page, sort expression, filter etc.
                return RedirectToAction("EditingServerSide", this.GridRouteValues());
            }

            //The model is invalid - render the current view to show any validation errors
            return View("EditingServerSide", SessionCustomerRepository.All());
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Delete(string id)
        {
            //Find a customer with CustomerID equal to the id action parameter
            EditableCustomer customer = SessionCustomerRepository.One(c => c.CustomerID == id);

            if (customer == null)
            {
                //A customer with the specified id does not exist - redisplay the grid

                //GridRouteValues() is an extension method which returns the 
                //route values defining the grid state - current page, sort expression, filter etc.
                return RedirectToAction("EditingServerSide", this.GridRouteValues());
            }
            
            //Delete the record
            SessionCustomerRepository.Delete(customer);

            //Redisplay the grid
            return RedirectToAction("EditingServerSide", this.GridRouteValues());
        }
    }
}