namespace Telerik.Web.Mvc.Examples
{
    using System.Web.Mvc;

    public partial class GridController : Controller
    {
        [SourceCodeFile("EditableCustomer (model)", "~/Models/EditableCustomer.cs")]
        [SourceCodeFile("Date.ascx (editor)", "~/Views/Shared/EditorTemplates/Date.ascx")]
        [SourceCodeFile("ASMX", "~/Models/Customers.asmx.cs")]
        [SourceCodeFile("WCF", "~/Models/Customers.svc.cs")]
        public ActionResult EditingWebService()
        {
            return View();
        }
    }
}
