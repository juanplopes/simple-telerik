namespace Telerik.Web.Mvc.Examples
{
    using System;
    using System.Text.RegularExpressions;
    using System.Reflection;
    using System.Web.Mvc;
    using System.IO;
    using Telerik.Web.Mvc.UI;
    using System.Web.Hosting;
    using System.Web;

    [PopulateProductSiteMap(SiteMapName = "examples", ViewDataKey = "telerik.mvc.examples")]
    public class HomeController : Controller
    {
        private static readonly Regex ForbiddenExtensions = new Regex("dll|config", RegexOptions.IgnoreCase);
        
        public ActionResult FirstLook()
        {
		    Assembly controlAssembly = typeof(Menu).Assembly;
		    Version version = controlAssembly.GetName().Version;

		    int quarter = version.Minor;
		    int versionYear = version.Major;
		    int year = versionYear;
		    int date = version.Build;
		    int month = date / 100;

		    if (month > 12)
		    {
			    year++;
			    month %= 12;
		    }

		    int day = date % 100;

		    ViewData["ProductVersion"] = string.Format("Q{0} {1}, released {2:d2}/{3:d2}/{4}",
                    quarter, versionYear, month, day, year);

            return View();
        }

        public ActionResult CodeFile(string file)
        {
            if (!file.StartsWith("~", StringComparison.OrdinalIgnoreCase))
            {
                return new EmptyResult();
            }

            file = Server.MapPath(file);
            string extension = Path.GetExtension(file);

            if (!System.IO.File.Exists(file) || ForbiddenExtensions.IsMatch(extension))
            {
                return new EmptyResult();
            }
            
            return PartialView((object)System.IO.File.ReadAllText(file));
        }
    }
}