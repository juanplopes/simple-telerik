// (c) Copyright 2002-2010 Telerik 
// This source is subject to the GNU General Public License, version 2
// See http://www.gnu.org/licenses/gpl-2.0.html. 
// All other rights reserved.

namespace Telerik.Web.Mvc.UI
{
    using System.Web.Mvc;
    using System.Web.Script.Serialization;

    using Extensions;
    using Infrastructure;
    using System;
    using System.Collections.Generic;

    public class TreeViewHtmlBuilder : NavigationHtmlBuilderBase<TreeView, TreeViewItem>, ITreeViewHtmlBuilder
    {
        public TreeViewHtmlBuilder(TreeView treeView, IActionMethodCache actionMethodCache)
            :base(treeView, actionMethodCache)
        {
        }

        public IHtmlNode TreeViewTag()
        {
            IHtmlNode div = ComponentTag("div")
                .PrependClass(UIPrimitives.Widget, "t-treeview", UIPrimitives.ResetStyle);

            if (Component.Items.Count > 0)
            {
                ListTag().ToggleClass(UIPrimitives.TreeView.Lines, Component.ShowLines)
                    .AppendTo(div);
            }

            return div;
        }

        public IHtmlNode ChildrenTag(TreeViewItem item)
        {
            return ListTag()
                .ToggleAttribute("style", "display:none", !item.Expanded);
        }

        public IHtmlNode ItemTag(TreeViewItem item)
        {
            IHtmlNode li = new HtmlTag("li")
                .Attributes(item.HtmlAttributes);

            if (item.NextSibling == null)
            {
                li.PrependClass(UIPrimitives.Last);
            }

            if (item.Parent == null && item.PreviousSibling == null)
            {
                li.PrependClass(UIPrimitives.First);
            }

            li.PrependClass(UIPrimitives.Item);

            IHtmlNode div = new HtmlTag("div")
                .ToggleClass(UIPrimitives.Top, item.PreviousSibling == null)
                .ToggleClass(UIPrimitives.Bottom, item.NextSibling == null)
                .ToggleClass(UIPrimitives.Middle, item.PreviousSibling != null && item.NextSibling != null)
                .AppendTo(li);

            if (((Component.Ajax.Enabled || Component.WebService.Enabled) && item.LoadOnDemand) || item.Items.Count > 0 || item.Content != null)
            {
                new HtmlTag("span")
                        .AddClass(UIPrimitives.Icon)
                        .ToggleClass("t-plus", item.Enabled && !item.Expanded)
                        .ToggleClass("t-minus", item.Enabled && item.Expanded)
                        .ToggleClass("t-plus-disabled", !item.Enabled && !item.Expanded)
                        .ToggleClass("t-minus-disabled", !item.Enabled && item.Expanded)
                        .AppendTo(div);
            }
            if (Component.ShowCheckBox)
            {
                string checkedItemNamePrefix = "checkedNodes[{0}].";

                IHtmlNode chkBoxWrapperTag = new HtmlTag("span")
                    .ToggleClass(UIPrimitives.DisabledState, !item.Enabled)
                    .AddClass(UIPrimitives.CheckBox)
                    .AppendTo(div);

                List<string> indexes = new List<string>();

                TreeViewItem currentItem = item;

                while (currentItem != null)
                {
                    indexes.Insert(0, currentItem.Parent == null ? Component.Items.IndexOf(currentItem).ToString() : currentItem.Parent.Items.IndexOf(currentItem).ToString());
                    currentItem = currentItem.Parent;
                }

                checkedItemNamePrefix = checkedItemNamePrefix.FormatWith(string.Join(":", indexes.ToArray()));

                new HtmlTag("input", TagRenderMode.SelfClosing)
                    .AddClass(UIPrimitives.Input)
                    .Attributes(new { type = "hidden", name = "checkedNodes.Index", value = string.Join(":", indexes.ToArray()) })
                    .AppendTo(chkBoxWrapperTag);
                
                IHtmlNode chkBoxTag = new HtmlTag("input", TagRenderMode.SelfClosing)
                    .AddClass(UIPrimitives.Input)
                    .Attributes(new { name = checkedItemNamePrefix + "Checked", type = "checkbox", value = item.Checked })
                    .AppendTo(chkBoxWrapperTag);

                if (item.Checked)
                {
                    chkBoxTag.Attribute("checked", "checked");

                    new HtmlTag("input", TagRenderMode.SelfClosing)
                    .AddClass(UIPrimitives.Input)
                    .Attributes(new { type = "hidden", name = checkedItemNamePrefix + "Text", value = item.Text })
                    .AppendTo(chkBoxWrapperTag);

                    new HtmlTag("input", TagRenderMode.SelfClosing)
                        .AddClass(UIPrimitives.Input)
                        .Attributes(new { type = "hidden", name = checkedItemNamePrefix + "Value", value = item.Value })
                        .AppendTo(chkBoxWrapperTag);
                }
            }

            if (((Component.Ajax.Enabled || Component.WebService.Enabled) && item.LoadOnDemand) || Component.ShowCheckBox)
            {
                new HtmlTag("input", TagRenderMode.SelfClosing)
                    .AddClass(UIPrimitives.Input)
                    .Attributes(new { type = "hidden", value = item.Value, name = "itemValue" })
                    .AppendTo(li);
            }

            return li;
        }

        public IHtmlNode ItemInnerContent(TreeViewItem item)
        {
            string url = Component.GetItemUrl(item, string.Empty);
            bool isNavigatable = !string.IsNullOrEmpty(url);

            IHtmlNode tag = new HtmlTag(isNavigatable ? "a" : "span");

            tag.PrependClass("t-in")
                .ToggleClass(UIPrimitives.DisabledState, !item.Enabled)
                .ToggleClass(UIPrimitives.SelectedState, item.Enabled && item.Selected);

            if (isNavigatable)
            {
                if (item.Enabled)
                {
                    tag.Attribute("href", url);
                }

                tag.Attributes(item.LinkHtmlAttributes);
                tag.PrependClass(UIPrimitives.Link);
            }

            if (!string.IsNullOrEmpty(item.ImageUrl))
            {
                ImageTag(item).AppendTo(tag);
            }

            if (!string.IsNullOrEmpty(item.SpriteCssClasses))
            {
                SpriteTag(item).AppendTo(tag);
            }

            Text(item).AppendTo(tag);

            return tag;
        }

        public IHtmlNode ItemContentTag(TreeViewItem item)
        {
            IHtmlNode div = ContentTag(item);

            if (!item.Expanded || !item.Enabled)
            {
                div.Attribute("style", "display:none");
            }

            return div;
        }
    }
}