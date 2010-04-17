namespace Telerik.Web.Mvc.Examples
{
    using System.Web.Mvc;
    using System;

    public partial class DatePickerController : Controller
    {
        public ActionResult ServerValidation()
        {
            return View();
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult ServerValidation(DateTime? deliveryDate)
        {
            // Validation logic
            if (deliveryDate == null)
            {
                ModelState.AddModelError("deliveryDate", "It is required to select a cake delivery date.");
            }

            if (ModelState.IsValid)
            {
                ViewData["deliveryDate"] = deliveryDate;
            }

            return View();
        }
    }
}