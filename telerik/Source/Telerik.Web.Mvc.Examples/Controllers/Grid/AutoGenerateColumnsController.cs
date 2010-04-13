namespace Telerik.Web.Mvc.Examples
{
    using System.Web.Mvc;
    
    using Models;

    public partial class GridController : Controller
    {
        public ActionResult AutoGenerateColumns()
        {
            return View(SessionCustomerRepository.All());
        }
    }
}