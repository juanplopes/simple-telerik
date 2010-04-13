namespace Telerik.Web.Mvc.Examples
{
    using System.Web.Mvc;

    public partial class GridController : Controller
    {
        [SourceCodeFile("TwitterItem.cs", "~/Models/TwitterItem.cs")]
        public ActionResult ExternalServiceTwitter()
        {
            return View();
        }
    }
}