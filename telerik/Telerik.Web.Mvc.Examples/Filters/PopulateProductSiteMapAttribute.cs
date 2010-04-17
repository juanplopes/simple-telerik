namespace Telerik.Web.Mvc.Examples
{
    using System.Linq;
    using System;
    using Telerik.Web.Mvc.Extensions;

    public class PopulateProductSiteMapAttribute : PopulateSiteMapAttribute
    {
        public override void OnResultExecuting(System.Web.Mvc.ResultExecutingContext filterContext)
        {
            base.OnResultExecuting(filterContext);

            XmlSiteMap fullSiteMap = (XmlSiteMap)filterContext.Controller.ViewData[ViewDataKey];
            XmlSiteMap productSiteMap = new XmlSiteMap();
            productSiteMap.RootNode = new SiteMapNode();

            foreach (SiteMapNode node in fullSiteMap.RootNode.ChildNodes)
            {
                string controller = node.ControllerName ?? node.Title;
                string action = node.ActionName ?? "firstlook";
                
                productSiteMap.RootNode.ChildNodes.Add(new SiteMapNode
                {
                    ActionName = action,
                    ControllerName = controller,
                    Title = node.Title
                });
            }

            filterContext.Controller.ViewData["telerik.web.mvc.products"] = productSiteMap;

            XmlSiteMap examplesSiteMap = new XmlSiteMap();
            
            string controllerName = (string)filterContext.RouteData.Values["controller"];
            SiteMapNode productSiteMapNode = fullSiteMap.RootNode.ChildNodes
                .FirstOrDefault(node => controllerName.Equals(node.Title, StringComparison.OrdinalIgnoreCase));

            if (productSiteMapNode != null && !controllerName.Equals("Home", StringComparison.OrdinalIgnoreCase))
            {
                examplesSiteMap.RootNode = new SiteMapNode();
                examplesSiteMap.RootNode.ChildNodes.Add(productSiteMapNode);
                filterContext.Controller.ViewData["telerik.web.mvc.products.examples"] = examplesSiteMap;
            }
        }
    }
}
