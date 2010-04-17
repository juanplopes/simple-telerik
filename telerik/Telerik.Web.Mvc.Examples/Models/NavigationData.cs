namespace Telerik.Web.Mvc.Examples.Models
{
    using System.Collections.Generic;

    public static class NavigationDataBuilder
    {
        public static List<NavigationData> GetCollection()
        {
            return new List<NavigationData>
            {
                new NavigationData
                {
                    Text = "ASP.NET MVC", 
                    ImageUrl = "~/Content/Common/Icons/Suites/mvc.png",
                    NavigateUrl = "http://www.telerik.com/products/aspnet-mvc.aspx"
                },
                new NavigationData
                {
                    Text = "Silverlight", 
                    ImageUrl = "~/Content/Common/Icons/Suites/sl.png",
                    NavigateUrl = "http://www.telerik.com/products/silverlight.aspx"
                },
                new NavigationData
                {
                    Text = "ASP.NET AJAX", 
                    ImageUrl = "~/Content/Common/Icons/Suites/ajax.png",
                    NavigateUrl = "http://www.telerik.com/products/aspnet-ajax.aspx"
                },
                new NavigationData
                {
                    Text = "OpenAccess ORM",
                    ImageUrl = "~/Content/Common/Icons/Suites/orm.png",
                    NavigateUrl = "http://www.telerik.com/products/orm.aspx"
                },
                new NavigationData
                {
                    Text = "Reporting", 
                    ImageUrl = "~/Content/Common/Icons/Suites/rep.png",
                    NavigateUrl = "http://www.telerik.com/products/reporting.aspx"
                },
                new NavigationData
                {
                    Text = "Sitefinity ASP.NET CMS", 
                    ImageUrl = "~/Content/Common/Icons/Suites/sitefinity.png",
                    NavigateUrl = "http://www.telerik.com/products/sitefinity.aspx"
                },
                new NavigationData
                {
                    Text = "Web Testing Tools", 
                    ImageUrl = "~/Content/Common/Icons/Suites/test.png",
                    NavigateUrl = "http://www.telerik.com/products/web-testing-tools.aspx"
                }
            };
        }
    }

    public class NavigationData
    {
        public string Text { get; set; }

        public string ImageUrl { get; set; }

        public string NavigateUrl { get; set; }
    }
}
