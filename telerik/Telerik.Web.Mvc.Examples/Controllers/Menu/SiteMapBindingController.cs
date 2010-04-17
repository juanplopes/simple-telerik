namespace Telerik.Web.Mvc.Examples
{
    using System.Web.Mvc;

    public partial class MenuController : Controller
    {
        [PopulateSiteMap(SiteMapName = "sample", ViewDataKey = "sample")]
        [SourceCodeFile("Sitemap", "~/sample.sitemap")]
        public ActionResult SiteMapBinding()
        {
            if (!SiteMapManager.SiteMaps.ContainsKey("sample"))
            {
                SiteMapManager.SiteMaps.Register<XmlSiteMap>("sample", sitmap => sitmap.LoadFrom("~/sample.sitemap"));
            }

            return View();
        }
    }
}