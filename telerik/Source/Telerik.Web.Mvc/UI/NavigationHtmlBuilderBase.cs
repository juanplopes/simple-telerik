// (c) Copyright 2002-2010 Telerik 
// This source is subject to the GNU General Public License, version 2
// See http://www.gnu.org/licenses/gpl-2.0.html. 
// All other rights reserved.

namespace Telerik.Web.Mvc.UI
{
    using Infrastructure;
    using Extensions;
    
    using System;
    using System.Web.Mvc;
    using System.Web;
    
    public abstract class NavigationHtmlBuilderBase<TComponent, TItem> : INavigationHtmlBuilder<TComponent, TItem>
        where TComponent : ViewComponentBase, INavigationItemComponent<TItem>
        where TItem : NavigationItem<TItem>
    {
        public NavigationHtmlBuilderBase(TComponent component, IActionMethodCache cache)
        {
            Component = component;
            ActionMethodCache = cache;
        }

        public TComponent Component
        {
            get;
            private set;
        }

        public IActionMethodCache ActionMethodCache
        {
            get;
            private set;
        }

        public IHtmlNode ListTag()
        {
            return new HtmlTag("ul")
                .AddClass(UIPrimitives.Group);
        }

        public IHtmlNode ComponentTag(string tagName)
        {
            return new HtmlTag(tagName)
                .Attribute("id", Component.Id)
                .Attributes(Component.HtmlAttributes);
        }

        public IHtmlNode ImageTag(TItem item)
        {
            return new HtmlTag("img", TagRenderMode.SelfClosing)
                    .Attributes(item.ImageHtmlAttributes)
                    .Attribute("alt", string.Empty, false)
                    .AddClass(UIPrimitives.Image)
                    .Attribute("src", item.GetImageUrl(Component.ViewContext));
        }

        public IHtmlNode Text(TItem item)
        {
            return new TextNode(Component.GetItemText(item, ActionMethodCache));
        }

        public IHtmlNode SpriteTag(TItem item)
        {
            return new HtmlTag("span")
                .AddClass(UIPrimitives.Sprite, item.SpriteCssClasses);
        }

        public IHtmlNode ContentTag(TItem item)
        {
            return new HtmlTag("div")
                .Attributes(item.ContentHtmlAttributes)
                .PrependClass(UIPrimitives.Content)
                .Template(item.Content)
                .Attribute("id", Component.GetItemContentId(item));
        }

        public IHtmlNode ListItemTag(TItem item, Action<IHtmlNode> configure)
        {
            IHtmlNode li = new HtmlTag("li")
                .Attributes(item.HtmlAttributes);

            if (!item.Enabled)
            {
                li.PrependClass(UIPrimitives.DisabledState);
            }
            else
            {
                configure(li);
            }

            return li.PrependClass(UIPrimitives.Item);
        }

        public IHtmlNode LinkTag(TItem item, Action<IHtmlNode> configure)
        {
            IHtmlNode a = new HtmlTag("a");

            if (item.Enabled)
            {
                string url = Component.GetItemUrl(item);
                a.Attribute("href", url);
            }

            a.Attributes(item.LinkHtmlAttributes);

            configure(a);

            a.PrependClass(UIPrimitives.Link);

            if (!string.IsNullOrEmpty(item.ImageUrl))
            {
                ImageTag(item).AppendTo(a);
            }
            
            if (!string.IsNullOrEmpty(item.SpriteCssClasses))
            {
                SpriteTag(item).AppendTo(a);
            }
            
            Text(item).AppendTo(a);

            return a;
        }
    }
}