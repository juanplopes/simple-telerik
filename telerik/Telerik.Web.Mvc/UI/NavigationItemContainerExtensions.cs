// (c) Copyright Telerik Corp. 
// This source is subject to the GNU General Public License, version 2
// See http://www.gnu.org/licenses/gpl-2.0.html. 
// All other rights reserved.

namespace Telerik.Web.Mvc.UI
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Web.Mvc;
    using Extensions;
    using Infrastructure;
    using Resources;

    public static class NavigationItemContainerExtensions
    {
        //where TComponent : ViewComponentBase, INavigationItemComponent<TItem>

        public static void WriteItem<TComponent, TItem>(this TItem item, TComponent component, IHtmlNode parentTag, INavigationComponentHtmlBuilder<TItem> builder)
            where TItem : NavigationItem<TItem>, IContentContainer, INavigationItemContainer<TItem>
            where TComponent : ViewComponentBase, INavigationItemComponent<TItem>
        {
            if (component.ItemAction != null)
            {
                component.ItemAction(item);
            }

            if (item.Visible && item.IsAccessible(component.Authorization, component.ViewContext))
            {
                var hasAccessibleChildren = item.Items.Any()
                                            && item.Items.IsAccessible(component.Authorization, component.ViewContext)
                                            && item.Items.Any(i => i.Visible);

                IHtmlNode itemTag = builder.ItemTag(item).AppendTo(parentTag);

                builder.ItemInnerContentTag(item, hasAccessibleChildren).AppendTo(itemTag);

                if (item.Template.HasValue() ||
                    (item is IAsyncContentContainer ? !string.IsNullOrEmpty(((IAsyncContentContainer)item).ContentUrl) : false))
                {
                    builder.ItemContentTag(item).AppendTo(itemTag);
                }
                else if (hasAccessibleChildren)
                {
                    IHtmlNode ul = builder.ChildrenTag(item).AppendTo(itemTag);

                    item.Items.Each(child => child.WriteItem(component, ul, builder));
                }
            }
        }

        public static string GetItemUrl<TComponent, TItem>(this TComponent component, TItem item)
            where TComponent : ViewComponentBase, INavigationItemComponent<TItem>
            where TItem : NavigationItem<TItem>, IContentContainer
        {
            return component.GetItemUrl(item, "#");
        }

        public static string GetItemUrl<TComponent, TItem>(this TComponent component, TItem item, string defaultValue)
            where TComponent : ViewComponentBase, INavigationItemComponent<TItem>
            where TItem : NavigationItem<TItem>, IContentContainer
        {
            IAsyncContentContainer asyncContentContainer = item as IAsyncContentContainer;

            if (asyncContentContainer != null && !asyncContentContainer.ContentUrl.IsNullOrEmpty())
            {
                return component.IsSelfInitialized ? System.Web.HttpUtility.UrlDecode(asyncContentContainer.ContentUrl) : asyncContentContainer.ContentUrl;
            }

            if (item.Template.HasValue() &&
                item.RouteName.IsNullOrEmpty() && item.Url.IsNullOrEmpty() &&
                item.ActionName.IsNullOrEmpty() && item.ControllerName.IsNullOrEmpty())
            {
                return "#" + component.GetItemContentId(item);
            }

            return item.GenerateUrl(component.ViewContext, component.UrlGenerator) ?? defaultValue;
        }

        public static string GetImageUrl<T>(this T item, ViewContext viewContext) where T : NavigationItem<T>
        {
            var urlHelper = new UrlHelper(viewContext.RequestContext);

            return urlHelper.Content(item.ImageUrl);
        }

        public static string GetItemContentId<TComponent, TItem>(this TComponent component, TItem item)
            where TComponent : ViewComponentBase, INavigationItemContainer<TItem>
            where TItem : NavigationItem<TItem>, IContentContainer
        {
            return item.ContentHtmlAttributes.ContainsKey("id") ?
                   "{0}".FormatWith(item.ContentHtmlAttributes["id"].ToString()) :
                   "{0}-{1}".FormatWith(component.Id, (component.Items.Where(i => i.Visible == true).IndexOf(item) + 1).ToString(Culture.Invariant));
        }

        public static string GetItemText<TComponent, TItem>(this TComponent component, TItem item, IActionMethodCache actionMethodCache)
            where TComponent : ViewComponentBase, INavigationItemContainer<TItem>
            where TItem : NavigationItem<TItem>, IContentContainer
        {
            string text = item.Text;

            if (string.IsNullOrEmpty(text) && ((!string.IsNullOrEmpty(item.ControllerName) && !string.IsNullOrEmpty(item.ActionName))))
            {
                foreach (MethodInfo method in actionMethodCache.GetActionMethods(component.ViewContext.RequestContext, item.ControllerName, item.ActionName))
                {
                    if (method != null)
                    {
                        string displayName = method.GetDisplayName();

                        if (!string.IsNullOrEmpty(displayName))
                        {
                            text = displayName;
                            break;
                        }
                    }
                }
            }

            return text;
        }

        public static void BindTo<T>(this INavigationItemComponent<T> component, string sitemapName, Action<T, SiteMapNode> siteMapAction) where T : NavigationItem<T>, new()
        {
            var siteMap = component.ViewContext.ViewData.Eval(sitemapName) as SiteMapBase ??
                 (SiteMapManager.SiteMaps.ContainsKey(sitemapName) ? SiteMapManager.SiteMaps[sitemapName] : null);

            if (siteMap == null)
            {
                throw new NotSupportedException(TextResource.SiteMapShouldBeDefinedInViewData.FormatWith(sitemapName));
            }

            component.Items.Clear();

            foreach (SiteMapNode node in siteMap.RootNode.ChildNodes)
            {
                LoadItemsFromSiteMapNode(node, component, siteMapAction);
            }
        }

        public static void BindTo<T>(this INavigationItemComponent<T> component, string sitemapViewDataKey) where T : NavigationItem<T>, new()
        {
            BindTo(component, sitemapViewDataKey, null);
        }

        public static void BindTo<TNavigationItem, TDataItem>(this INavigationItemComponent<TNavigationItem> component, IEnumerable<TDataItem> dataSource, Action<TNavigationItem, TDataItem> action) where TNavigationItem : NavigationItem<TNavigationItem>, new()
        {
            foreach (TDataItem dataItem in dataSource)
            {
                var item = new TNavigationItem();

                component.Items.Add(item);

                action(item, dataItem);
            }
        }

        public static void BindTo<TNavigationItem>(this INavigationItemContainer<TNavigationItem> component,
            IEnumerable dataSource, Action<NavigationBindingFactory<TNavigationItem>> factoryAction)
                where TNavigationItem : NavigationItem<TNavigationItem>, INavigationItemContainer<TNavigationItem>, new()
        {
            NavigationBindingFactory<TNavigationItem> factory = new NavigationBindingFactory<TNavigationItem>();

            factoryAction(factory);

            foreach (object dataItem in dataSource)
            {
                TNavigationItem item = new TNavigationItem();
                component.Items.Add(item);
                Bind(item, dataItem, factory);
            }
        }

        public static void Bind<TNavigationItem>(TNavigationItem component, object dataItem,
            NavigationBindingFactory<TNavigationItem> factory)
                where TNavigationItem : NavigationItem<TNavigationItem>, INavigationItemContainer<TNavigationItem>, new()
        {
            INavigationBinding<TNavigationItem> binding = factory.container.Where(b => b.Type.IsAssignableFrom(dataItem.GetType())).First();

            binding.ItemDataBound(component, dataItem);
            IEnumerable children = binding.Children(dataItem);

            if (children != null)
            {
                foreach (var childDataItem in children)
                {
                    TNavigationItem item = new TNavigationItem();
                    component.Items.Add(item);

                    Bind(item, childDataItem, factory);
                }
            }
        }

        private static void LoadItemsFromSiteMapNode<T>(SiteMapNode node, INavigationItemContainer<T> parent, Action<T, SiteMapNode> siteMapAction)
            where T : NavigationItem<T>, new()
        {
            var item = new T { Text = node.Title, Visible = node.Visible };

            if (!string.IsNullOrEmpty(node.RouteName))
            {
                item.RouteName = node.RouteName;
            }

            if (!string.IsNullOrEmpty(node.ControllerName) && !string.IsNullOrEmpty(node.ActionName))
            {
                item.ControllerName = node.ControllerName;
                item.ActionName = node.ActionName;
            }

            if (!string.IsNullOrEmpty(node.Url))
            {
                item.Url = node.Url;
            }

            item.RouteValues.AddRange(node.RouteValues);

            if (siteMapAction != null)
            {
                siteMapAction(item, node);
            }

            parent.Items.Add(item);

            if (item is INavigationItemContainer<T>)
            {
                node.ChildNodes.Each(childNode => LoadItemsFromSiteMapNode(childNode, (INavigationItemContainer<T>)item, siteMapAction));
            }
        }
    }
}
