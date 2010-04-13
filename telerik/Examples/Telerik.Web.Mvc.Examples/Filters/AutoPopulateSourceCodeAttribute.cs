namespace Telerik.Web.Mvc.Examples
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Web;
    using System.Web.Mvc;

    [AttributeUsage(AttributeTargets.Class, Inherited = true, AllowMultiple = false)]
    public class AutoPopulateSourceCodeAttribute : FilterAttribute, IResultFilter
    {
        private const string ViewPath = "~/Views/";
        private const string ControllerPath = "~/Controllers/";
        private const string DescriptionPath = "~/Content/";
        private const string MasterPagePath = "~/Views/Shared/Site.Master";

        public void OnResultExecuting(ResultExecutingContext filterContext)
        {
            ViewResult viewResult = filterContext.Result as ViewResult;

            if (viewResult != null)
            {
                HttpServerUtilityBase server = filterContext.HttpContext.Server;

                string controllerName = filterContext.RouteData.GetRequiredString("controller");

                string viewName =
                    !string.IsNullOrEmpty(viewResult.ViewName)
                        ? viewResult.ViewName
                        : filterContext.RouteData.GetRequiredString("action");

                string viewPath = ViewPath + controllerName + Path.AltDirectorySeparatorChar + viewName + ".aspx";

                string exampleControllerPath = ControllerPath + controllerName + Path.AltDirectorySeparatorChar + viewName + "Controller.cs";

                string descriptionPath =
                    server.MapPath(DescriptionPath + Path.AltDirectorySeparatorChar + controllerName + 
                    Path.AltDirectorySeparatorChar + "Descriptions" + 
                    Path.AltDirectorySeparatorChar + viewName + ".html");

                string masterPagePath = MasterPagePath;

                var codeFiles = new Dictionary<string, string>();

                var viewData = filterContext.Controller.ViewData;

                if (System.IO.File.Exists(descriptionPath))
                {
                    viewData["Description"] = System.IO.File.ReadAllText(descriptionPath);
                }

                codeFiles["View"] = viewPath;

                codeFiles["Controller"] = exampleControllerPath;
                codeFiles["Site.Master"] = masterPagePath;
                viewData["codeFiles"] = codeFiles;
            }
        }

        public void OnResultExecuted(ResultExecutedContext filterContext)
        {
            // Do Nothing
        }
    }
}