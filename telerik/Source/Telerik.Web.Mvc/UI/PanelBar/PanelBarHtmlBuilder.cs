using Telerik.Web.Mvc.Infrastructure;
// (c) Copyright 2002-2010 Telerik 
// This source is subject to the GNU General Public License, version 2
// See http://www.gnu.org/licenses/gpl-2.0.html. 
// All other rights reserved.

namespace Telerik.Web.Mvc.UI
{
    using System.Web.UI;
    using System.Reflection;

    using Extensions;
    using Infrastructure;
    using System.Web.Mvc;

    public class PanelBarHtmlBuilder : NavigationHtmlBuilderBase<PanelBar,PanelBarItem>, IPanelBarHtmlBuilder
    {
        public PanelBarHtmlBuilder(PanelBar panelBar, IActionMethodCache actionMethodCache)
            : base(panelBar, actionMethodCache)
        {
        }

        public IHtmlNode ChildrenTag(PanelBarItem item)
        {
            IHtmlNode ul = ListTag();

            if(!item.Enabled)
                ul.Attribute("style", "display:none");
            else if (item.Enabled && !item.Expanded)
                ul.Attribute("style", "display:none");

            return ul;
        }

        public IHtmlNode PanelBarTag()
        {
            return ComponentTag("ul")
                .PrependClass(UIPrimitives.Widget, "t-panelbar", UIPrimitives.ResetStyle);
        }

        public IHtmlNode ItemTag(PanelBarItem item)
        {
            return ListItemTag(item, li =>
            {
                if (item.Expanded)
                {
                    li.PrependClass(UIPrimitives.ActiveState);
                }
                else if (!item.Selected)
                {
                    li.PrependClass(UIPrimitives.DefaultState);
                }
            });
        }

        public IHtmlNode ItemInnerTag(PanelBarItem item)
        {
            IHtmlNode a = LinkTag(item, tag =>
            {
                if (item.Parent == null)
                {
                    tag.PrependClass(UIPrimitives.Header);
                }

                if (item.Parent != null && item.Selected)
                {
                    tag.PrependClass(UIPrimitives.SelectedState);
                }
            });

            if (item.Items.Count > 0 || item.Content != null || !string.IsNullOrEmpty(item.ContentUrl))
            {
                new HtmlTag("span")
                    .AddClass(UIPrimitives.Icon)
                    .ToggleClass("t-arrow-up", item.Enabled && item.Expanded)
                    .ToggleClass("t-arrow-down", item.Enabled && !item.Expanded)
                    .AppendTo(a);
            }

            return a;
        }

        public IHtmlNode ItemContentTag(PanelBarItem item)
        {
            IHtmlNode div = ContentTag(item);
            
            if (!item.Expanded || !string.IsNullOrEmpty(item.ContentUrl) || !item.Enabled)
            {
                div.Attribute("style", "display:none");
            }

            return div;
        }
    }
}
