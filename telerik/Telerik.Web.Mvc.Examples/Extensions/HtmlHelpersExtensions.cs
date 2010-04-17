namespace Telerik.Web.Mvc.Examples
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Web.Mvc;
    using System.Web.Mvc.Html;
    using System.Web.UI;
    using Telerik.Web.Mvc.Extensions;

    public static class HtmlHelpersExtensions
    {
        private static IDictionary<string, int> controllerToProductIdMap = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase)
            {
                { "grid", 718 },
                { "menu", 719 },
                { "panelbar", 720 },
                { "tabstrip", 721 }
            };

        public static ExampleConfigurator Configurator(this HtmlHelper instance, string title)
        {
            return new ExampleConfigurator(instance)
                        .Title(title);
        }
        
        public static string ExampleTitle(this HtmlHelper helper)
        {
            XmlSiteMap sitemap = (XmlSiteMap)helper.ViewData["telerik.mvc.examples"];
            string controller = (string)helper.ViewContext.RouteData.Values["controller"];
            string action = (string)helper.ViewContext.RouteData.Values["action"];

            SiteMapNode exampleSiteMapNode = sitemap.RootNode.ChildNodes
                .SelectRecursive(node => node.ChildNodes)
                .FirstOrDefault(node => controller.Equals(node.ControllerName, StringComparison.OrdinalIgnoreCase) &&
                    action.Equals(node.ActionName, StringComparison.OrdinalIgnoreCase));

            if (exampleSiteMapNode != null)
            {
                return exampleSiteMapNode.Title;
            }

            return string.Empty;
        }

        public static string ProductMetaTag(this HtmlHelper instance)
        {
            string controller = (string)instance.ViewContext.RouteData.Values["controller"];

            if (!controllerToProductIdMap.ContainsKey(controller))
            {
                return string.Empty;
            }

            return String.Format("<meta name=\"ProductId\" content=\"{0}\" />", controllerToProductIdMap[controller]);
        }

        public static string CheckBox(this HtmlHelper instance, string id, bool isChecked, string labelText)
        {
            return (new StringBuilder())
                        .Append(instance.CheckBox(id, isChecked))
                        .Append("<label for=\"")
                        .Append(id)
                        .Append("\">")
                        .Append(labelText)
                        .Append("</label>")
                    .ToString();
        }

        public static string GetCurrentTheme(this HtmlHelper instance)
        {
            return instance.ViewContext.HttpContext.Request.QueryString["theme"] ?? "vista";
        }
    }

    public class ExampleConfigurator : IDisposable
    {
        public const string CssClass = "configurator";

        private HtmlTextWriter writer;
        private HtmlHelper htmlHelper;
        private string title;
        private MvcForm form;

        public ExampleConfigurator(HtmlHelper instance)
        {
            this.htmlHelper = instance;
            this.writer = new HtmlTextWriter(instance.ViewContext.HttpContext.Response.Output);
        }

        public ExampleConfigurator Title(string title)
        {
            this.title = title;

            return this;
        }

        public ExampleConfigurator Begin()
        {
            this.writer.AddAttribute(HtmlTextWriterAttribute.Class, CssClass);
            this.writer.RenderBeginTag(HtmlTextWriterTag.Div);

            this.writer.AddAttribute(HtmlTextWriterAttribute.Class, CssClass + "-legend");
            this.writer.RenderBeginTag(HtmlTextWriterTag.H4);
            this.writer.Write(this.title);
            this.writer.RenderEndTag();

            return this;
        }

        public ExampleConfigurator End()
        {
            this.writer.RenderEndTag(); // fieldset

            if (this.form != null)
            {
                this.form.EndForm();
            }

            return this;
        }

        public void Dispose()
        {
            this.End();
        }

        public ExampleConfigurator PostTo(string action, string controller)
        {
            string theme = this.htmlHelper.ViewContext.HttpContext.Request.Params["theme"] ?? "vista";

            this.form = this.htmlHelper.BeginForm(action, controller, new { theme = theme });

            return this;
        }
    }
}