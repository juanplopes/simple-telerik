namespace Telerik.Web.Mvc.Examples
{
    using System;
    using System.Web.Mvc;
    using Telerik.Web.Mvc.Examples.Models;

    public partial class DatePickerController : Controller
    {
        [SourceCodeFile("Date.ascx", "~/Views/Shared/EditorTemplates/Date.ascx")]
        [SourceCodeFile("EditableProduct.cs", "~/Models/EditableProduct.cs")]
        public ActionResult EditTemplate(EditableProduct product)
        {
            if (product.ProductID == 0)
            {
                product.ProductID = 1;
                product.ProductName = "Chai";
                product.UnitsInStock = 39;
                product.UnitPrice =  18.00m;
                product.LastSupply = DateTime.Today;
            }
            return View(product);
        }
    }
}