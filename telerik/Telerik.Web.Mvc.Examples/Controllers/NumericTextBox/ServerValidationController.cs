namespace Telerik.Web.Mvc.Examples
{
    using System.Web.Mvc;
    using System;

    public partial class NumericTextBoxController : Controller
    {
        public ActionResult ServerValidation()
        {
            return View();
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult ServerValidation(double? piecesOfCake)
        {
            // Validation logic
            if (piecesOfCake == null)
            {
                ModelState.AddModelError("piecesOfCake", "It is required that you order cake.");
            }

            if (ModelState.IsValid)
            {
                ViewData["piecesOfCake"] = piecesOfCake;
            }

            return View();
        }
    }
}