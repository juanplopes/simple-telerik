namespace Telerik.Web.Mvc.Examples
{
    using System.Web;
    using System.Web.Mvc;
    using System.Web.Routing;

    public class MvcApplication : HttpApplication
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "CustomRoute",
                "{controller}/customroute/{page}/{orderBy}/{filter}",
                new { controller = "Grid", action = "CustomRoute", page = 1, orderBy = "", filter = "" });

            routes.MapRoute(
                "Default",
                "{controller}/{action}/{id}",
                new { controller = "Home", action = "FirstLook", id = "" });
        }

        protected void Application_Start()
        {
            RegisterRoutes(RouteTable.Routes);

            SiteMapManager.SiteMaps.Register<XmlSiteMap>("examples", sitmap => sitmap.Load());
        }
    }
}