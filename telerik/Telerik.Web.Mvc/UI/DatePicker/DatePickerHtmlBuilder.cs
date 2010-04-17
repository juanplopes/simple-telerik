using Telerik.Web.Mvc.Infrastructure;
// (c) Copyright 2002-2010 Telerik 
// This source is subject to the GNU General Public License, version 2
// See http://www.gnu.org/licenses/gpl-2.0.html. 
// All other rights reserved.

namespace Telerik.Web.Mvc.UI
{
    using System;
    using System.Web.UI;
    using System.Web.Mvc;

    using Extensions;
    using Infrastructure;
    
    public class DatePickerHtmlBuilder : IDatePickerHtmlBuilder
    {
        public DatePickerHtmlBuilder(DatePicker datePicker)
        {
            DatePicker = datePicker;
        }

        public DatePicker DatePicker 
        { 
            get; 
            private set; 
        }

        public IHtmlNode Build()
        {
            return new HtmlTag("div")
                .Attribute("id", DatePicker.Id)
                .Attributes(DatePicker.HtmlAttributes)
                .AddClass(UIPrimitives.Widget, "t-datepicker");
        }

        public IHtmlNode InputTag()
        {
            ModelState state;
            DateTime? date = null;
            ViewDataDictionary viewData = DatePicker.ViewContext.ViewData;
            if (viewData.ModelState.TryGetValue(DatePicker.Id, out state))
            {
                if (state.Errors.Count == 0)
                {
                    date = state.Value.ConvertTo(typeof(DateTime), null) as DateTime?;
                }
            }

            else if (DatePicker.Value != DateTime.MinValue)
            {
                date = DatePicker.Value;
            }

            object valueFromViewData = viewData.Eval(DatePicker.Name);

            if (valueFromViewData != null)
            {
                date = Convert.ToDateTime(valueFromViewData);
            }

            string value = string.Empty;

            if (date != null)
            {
                if (string.IsNullOrEmpty(DatePicker.Format))
                {
                    value = date.Value.ToShortDateString();
                }
                else
                {
                    value = date.Value.ToString(DatePicker.Format);
                }
            }

            return new HtmlTag("input", TagRenderMode.SelfClosing)
                .AddClass(UIPrimitives.Input)
                .Attributes(new { name = DatePicker.Name, id = DatePicker.Id + "-input", value = value })
                .Attributes(DatePicker.InputHtmlAttributes);
        }

        public IHtmlNode ButtonTag()
        {
            string title = string.IsNullOrEmpty(DatePicker.ButtonTitle) ? "Open the calendar" : DatePicker.ButtonTitle;

            return new HtmlTag("a")
                .Attributes(new { href = "#", title = title, tabindex = "-1" })
                .AddClass(UIPrimitives.Link, UIPrimitives.Icon, "t-icon-calendar")
                .Text("select date");
        }
    }
}
