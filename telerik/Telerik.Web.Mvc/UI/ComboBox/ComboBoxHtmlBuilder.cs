// (c) Copyright 2002-2010 Telerik 
// This source is subject to the GNU General Public License, version 2
// See http://www.gnu.org/licenses/gpl-2.0.html. 
// All other rights reserved.

namespace Telerik.Web.Mvc.UI
{
    using System.Linq;

    using Extensions;
    using Infrastructure;
    
    public class ComboBoxHtmlBuilder : IDropDownHtmlBuilder
    {

        public ComboBoxHtmlBuilder(IComboBoxRenderable component)
        {
            this.Component = component;
        }

        public IComboBoxRenderable Component
        {
            get;
            private set;
        }

        public IHtmlNode Build()
        {
            return new HtmlTag("div")
                    .Attribute("id", Component.Id)
                    .Attributes(Component.HtmlAttributes)
                    .PrependClass(UIPrimitives.Widget, "t-combobox", UIPrimitives.Header);
        }

        public IHtmlNode InnerContentTag()
        {
            IHtmlNode root = new HtmlTag("div").AddClass("t-dropdown-wrap t-state-default");

            IHtmlNode input = new HtmlTag("input")
                    .Attributes(new { type = "text",
                                      title = Component.Id })
                    .Attributes(Component.InputHtmlAttributes)
                    .PrependClass(UIPrimitives.Input)
                    .AppendTo(root);

            if(Component.Items.Any() && Component.SelectedIndex != -1) 
            {
                input.Attribute("value", Component.Items[Component.SelectedIndex].Text);
            }

            if (Component.Id.HasValue())
            {
                input.Attributes(new
                {
                    id = Component.Id + "-input",
                    name = Component.Name + "-input"
                });

                string value = Component.ViewContext.Controller.ValueOf<string>(Component.Name + "-input");
                if (value.HasValue())
                {
                    input.Attribute("value", value);
                }
            }

            IHtmlNode link = new HtmlTag("span").AddClass("t-select", UIPrimitives.Header);

            new HtmlTag("span").AddClass(UIPrimitives.Icon, "t-arrow-down").Html("select").AppendTo(link);

            link.AppendTo(root);

            return root;
        }

        public IHtmlNode HiddenInputTag()
        {
            IHtmlNode input = new HtmlTag("input")
                    .Attributes(new { type = "text", 
                                      style="display:none" });

            if (Component.Items.Any() && Component.SelectedIndex != -1)
            {
                input.Attribute("value", Component.Items[Component.SelectedIndex].Value);
            }

            if (Component.Name.HasValue()) { 
                input.Attributes(new { name = Component.Name,
                                       id = Component.Id + "-value" });

                string value = Component.ViewContext.Controller.ValueOf<string>(Component.Name);
                if (value.HasValue())
                {
                    input.Attribute("value", value);
                }
            }
            
            return input;
        }
    }
}
