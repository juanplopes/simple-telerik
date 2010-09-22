// (c) Copyright 2002-2010 Telerik 
// This source is subject to the GNU General Public License, version 2
// See http://www.gnu.org/licenses/gpl-2.0.html. 
// All other rights reserved.

namespace Telerik.Web.Mvc.UI
{
    using System.Web.Mvc;
    using Extensions;
    using Infrastructure;
    
    public class WindowHtmlBuilder : IWindowHtmlBuilder
    {
        public WindowHtmlBuilder(Window window)
        {
            Window = window;
        }

        public Window Window 
        { 
            get;
            private set;
        }

        public IHtmlNode WindowTag()
        {
            return new HtmlTag("div")
                   .Attribute("style", !Window.Visible ? "display:none" : string.Empty)
                   .Attribute("id", Window.Id)
                   .Attributes(Window.HtmlAttributes)
                   .PrependClass(UIPrimitives.Widget, "t-window");
        }

        public IHtmlNode HeaderTag()
        {
            IHtmlNode header = new HtmlTag("div")
                  .AddClass("t-window-titlebar", UIPrimitives.Header);

            new LiteralNode("&nbsp;").AppendTo(header);

            return header;
        }

        public IHtmlNode IconTag()
        {
            return new HtmlTag("img", TagRenderMode.SelfClosing)
                    .Attribute("alt", Window.IconAlternativeText.HasValue() ? Window.IconAlternativeText : "icon", false)
                    .AddClass(UIPrimitives.Image, "t-window-icon")
                    .Attribute("src", Window.IconUrl);
        }

        public IHtmlNode TitleTag() 
        {
            IHtmlNode title = new HtmlTag("span")
                   .AddClass("t-window-title");

            if (!Window.IconUrl.IsNullOrEmpty())
                IconTag().AppendTo(title);

            new TextNode(Window.Title.IsNullOrEmpty() ? Window.Name : Window.Title).AppendTo(title);

            return title;
        }

        public IHtmlNode ButtonContainerTag()
        {
            return new HtmlTag("div").AddClass("t-window-actions t-header");
        }

        public IHtmlNode ButtonTag(IWindowButton button)
        {
            IHtmlNode linkTag = new HtmlTag("a")
                                .AddClass("t-window-action", UIPrimitives.Link)
                                .Attribute("href", "#");

            linkTag.Children.Add(new HtmlTag("span")
                            .AddClass(UIPrimitives.Icon, button.CssClass)
                            .Html(button.Name));


            return linkTag;
        }

        public IHtmlNode ContentTag()
        {
            var content = new HtmlTag("div")
                               .AddClass("t-window-content", UIPrimitives.Content)
                               .Css("overflow", Window.Scrollable ? "auto" : "hidden")
                               .Attributes(Window.ContentHtmlAttributes);

            if (Window.Width != 0)
            {
                content.Css("width", Window.Width + "px");
            }

            if (Window.Height != 0)
            {
                content.Css("height", Window.Height + "px");
            }

            if (Window.ContentUrl.HasValue()
                && (Window.ContentUrl.StartsWith("http", System.StringComparison.InvariantCultureIgnoreCase)
                ||  Window.ContentUrl.StartsWith("https", System.StringComparison.InvariantCultureIgnoreCase)))
            {
                new HtmlTag("iframe")
                    .Attributes(new {
                        src = Window.ContentUrl,
                        title = Window.Title,
                        style = "border: 0; width: 100%; height: 100%;",
                        frameborder = "0"
                    })
                    .AppendTo(content);
            } 
            else if (Window.Template.HasValue())
            {
                Window.Template.Apply(content);
            }
            
            return content;
        }

    }
}