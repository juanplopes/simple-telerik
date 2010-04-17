using Telerik.Web.Mvc.Infrastructure;
// (c) Copyright 2002-2010 Telerik 
// This source is subject to the GNU General Public License, version 2
// See http://www.gnu.org/licenses/gpl-2.0.html. 
// All other rights reserved.

namespace Telerik.Web.Mvc.UI
{
    using System.Reflection;
    using System.Web.Mvc;
    using System.Web.UI;

    using Extensions;
    using Infrastructure;

    public class MenuHtmlBuilder : NavigationHtmlBuilderBase<Menu, MenuItem>, IMenuHtmlBuilder
    {
        public MenuHtmlBuilder(Menu menu, IActionMethodCache actionMethodCache)
            : base(menu, actionMethodCache)
        {
        }

        public IHtmlNode ChildrenTag()
        {
            return ListTag();
        }

        public IHtmlNode MenuTag()
        {
            IHtmlNode rootTag = ComponentTag("ul")
                .PrependClass(UIPrimitives.Widget, UIPrimitives.ResetStyle, UIPrimitives.Header, "t-menu")
                .ToggleClass("t-menu-vertical", Component.Orientation == MenuOrientation.Vertical);

            return rootTag;
        }

        public IHtmlNode ItemTag(MenuItem item)
        {
            return ListItemTag(item, li =>
            {
                if (item.Selected)
                {
                    li.AddClass(UIPrimitives.SelectedState);
                }
                else
                {
                    li.AddClass(UIPrimitives.DefaultState);
                }
            });
        }

        public IHtmlNode ItemContentTag(MenuItem item)
        {
            IHtmlNode ul = ListTag();

            IHtmlNode li = new HtmlTag("li")
                .AddClass(UIPrimitives.Item)
                .AppendTo(ul);

            ContentTag(item)
                .AppendTo(li);

            return ul;
        }

        public IHtmlNode ItemInnerContentTag(MenuItem item)
        {
            IHtmlNode a = this.LinkTag(item, delegate { });

            if (item.Items.Count > 0 || item.Content != null)
            {
                string iconClass = "t-arrow-next";

                if (Component.Orientation == MenuOrientation.Horizontal && item.Parent == null)
                {
                    iconClass = "t-arrow-down";
                }

                new HtmlTag("span")
                    .AddClass(UIPrimitives.Icon, iconClass)
                    .AppendTo(a);
            }

            return a;
        }
    }
}
