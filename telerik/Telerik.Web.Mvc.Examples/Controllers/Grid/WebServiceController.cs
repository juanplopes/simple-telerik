namespace Telerik.Web.Mvc.Examples
{
    using System.Web.Mvc;

    public partial class GridController : Controller
    {
        [SourceCodeFile("Order DTO", "~/Models/OrderDto.cs")]
        [SourceCodeFile("ASMX Web Service", "~/Models/Orders.asmx.cs")]
        [SourceCodeFile("WCF Web Service", "~/Models/Orders.svc.cs")]
        public ActionResult WebService()
        {
            return View();
        }
    }
}