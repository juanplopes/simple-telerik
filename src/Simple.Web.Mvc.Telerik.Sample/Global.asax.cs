using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Simple.Web.Mvc.Telerik.Sample.Config;
using NHibernate.Type;
using System.EnterpriseServices;
using NHibernate.Tool.hbm2ddl;
using Simple.Web.Mvc.Telerik.Sample.Model;

namespace Simple.Web.Mvc.Telerik.Sample
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801


    public class MvcApplication : System.Web.HttpApplication
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "Default",                                              // Route name
                "{controller}/{action}/{id}",                           // URL with parameters
                new { controller = "Home", action = "Index", id = "" }  // Parameter defaults
            );

        }

        protected void Application_Start()
        {
            new Configurator().StartServer<Configurator>();

            var cfg = Simply.Do.GetNHibernateConfig();

            var check = new SchemaValidator(cfg);

            try
            {
                check.Validate();
            }
            catch
            {
                var exp = new SchemaExport(Simply.Do.GetNHibernateConfig());
                exp.Drop(true, true);
                exp.Create(true, true);

                using (Simply.Do.EnterContext())
                {
                    UserSample.Init();
                    GroupSample.Init();
                }
            }

            RegisterRoutes(RouteTable.Routes);
        }

        protected void Application_BeginRequest()
        {
            Simply.Do.EnterContext();
        }

        protected void Application_EndRequest()
        {
            Simply.Do.GetContext().Exit();
        }

    }
}