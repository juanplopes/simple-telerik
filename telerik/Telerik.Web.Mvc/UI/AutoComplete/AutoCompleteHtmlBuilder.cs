// (c) Copyright 2002-2009 Telerik 
// This source is subject to the GNU General Public License, version 2
// See http://www.gnu.org/licenses/gpl-2.0.html. 
// All other rights reserved.

namespace Telerik.Web.Mvc.UI
{
    using Extensions;
    using Infrastructure;

    public class AutoCompleteHtmlBuilder : IAutoCompleteHtmlBuilder
    {

        public AutoCompleteHtmlBuilder(AutoComplete component)
        {
            this.Component = component;
        }

        public AutoComplete Component
        {
            get;
            private set;
        }

        public IHtmlNode Build()
        {
            IHtmlNode input = new HtmlTag("input")
                            .Attributes(new
                            {
                                title = Component.Name,
                                id = Component.Id,
                                autocomplete = "off",
                                name = Component.Name
                            })
                            .Attributes(Component.HtmlAttributes)
                            .PrependClass(UIPrimitives.Widget, "t-autocomplete", UIPrimitives.Input);

            string value = Component.ViewContext.Controller.ValueOf<string>(Component.Name);
            if (value.HasValue())
                input.Attribute("value", value);

            return input;
        }
    }
}