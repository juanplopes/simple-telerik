namespace Telerik.Web.Mvc.Examples
{
    using System.Web.Mvc;
    using Telerik.Web.Mvc.Examples.Models;

    [HandleError]
    public partial class DatePickerController : Controller
    {
        public ActionResult ClientValidation()
        {
            OrderDto dto =
                new OrderDto
                {
                    OrderID = 1,
                    ContactName = "Vincent",
                    OrderDate = null,
                    ShipAddress = "Stadhouderskade 55,\n1072 AB Amsterdam"
                };

            return View(dto);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult ClientValidation(OrderDto dto)
        {
            return View(dto);
        }
    }
}