// (c) Copyright 2002-2010 Telerik 
// This source is subject to the GNU General Public License, version 2
// See http://www.gnu.org/licenses/gpl-2.0.html. 
// All other rights reserved.

namespace Telerik.Web.Mvc.UI
{
    using System;
    using System.Web.Mvc;
    using Extensions;
    using Infrastructure;

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
                    .Attribute("alt", "image", false)
                    .Attributes(item.ImageHtmlAttributes)
                    .PrependClass(UIPrimitives.Image)
                    .Attribute("src", item.GetImageUrl(Component.ViewContext));
        }

        public IHtmlNode Text(TItem item)
        {
            if (item.Encoded)
                return new TextNode(Component.GetItemText(item, ActionMethodCache));
            else
                return new LiteralNode(Component.GetItemText(item, ActionMethodCache));
        }

        public IHtmlNode SpriteTag(TItem item)
        {
            return new HtmlTag("span")
                .AddClass(UIPrimitives.Sprite, item.SpriteCssClasses);
        }

        public IHtmlNode ContentTag(TItem item)
        {
            var content = new HtmlTag("div").Attributes(item.ContentHtmlAttributes)
                .PrependClass(UIPrimitives.Content)
                .Attribute("id", Component.GetItemContentId(item));
            
            if (item.Template.HasValue())
            {
                item.Template.Apply(content);
            }

            return content;
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
            var url = Component.GetItemUrl(item);

            IHtmlNode a;

            if (url != "#")
            {
                a = new HtmlTag("a");

                a.Attribute("href", url);
            }
            else
            {
                a = new HtmlTag("span");
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