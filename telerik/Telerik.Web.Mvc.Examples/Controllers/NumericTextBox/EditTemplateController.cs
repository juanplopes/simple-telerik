namespace Telerik.Web.Mvc.Examples
{
    using System;
    using System.Web.Mvc;
    using Telerik.Web.Mvc.Examples.Models;

    public partial class NumericTextBoxController : Controller
    {
        [SourceCodeFile("Currency.ascx", "~/Views/Shared/EditorTemplates/Currency.ascx")]
        [SourceCodeFile("Integer.ascx", "~/Views/Shared/EditorTemplates/Integer.ascx")]
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