// (c) Copyright 2002-2010 Telerik 
// This source is subject to the GNU General Public License, version 2
// See http://www.gnu.org/licenses/gpl-2.0.html. 
// All other rights reserved.

namespace Telerik.Web.Mvc.UI.Html
{
    using Telerik.Web.Mvc.Infrastructure;
    
    public class GridPagerButtonHtmlBuilder : HtmlBuilderBase
    {
        public GridPagerButtonHtmlBuilder(string text)
        {
            Enabled = true;
            Text = text;
        }

        public string Url
        {
            get;
            set;
        }
        public bool Enabled
        {
            get;
            set;
        }

        public string Text
        {
            get;
            set;
        }

        protected override IHtmlNode BuildCore()
        {
            var a = new HtmlTag("a")
                .AddClass(UIPrimitives.Link)
                .ToggleClass(UIPrimitives.DisabledState, !Enabled)
                .Attribute("href", Url)
                .ToggleAttribute("href", "#", !Enabled);

            var span = new HtmlTag("span")
                        .AddClass(UIPrimitives.Icon, "t-arrow-" + Text)
                        .Text(Text);

            span.AppendTo(a);

            return a;
        }
    }
}
