// (c) Copyright 2002-2010 Telerik 
// This source is subject to the GNU General Public License, version 2
// See http://www.gnu.org/licenses/gpl-2.0.html. 
// All other rights reserved.

namespace Telerik.Web.Mvc.UI
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
    
    using Extensions;
    using Infrastructure;
    using Resources;
    
    public class Calendar : ViewComponentBase
    {
        private readonly ICalendarHtmlBuilderFactory rendererFactory;

        private string urlFormat;

        public Calendar(ViewContext viewContext, IClientSideObjectWriterFactory clientSideObjectWriterFactory, IUrlGenerator urlGenerator, ICalendarHtmlBuilderFactory rendererFactory)
            : base(viewContext, clientSideObjectWriterFactory)
        {
            UrlGenerator = urlGenerator;

            ScriptFileNames.AddRange(new[] { "telerik.common.js", "telerik.calendar.js" });

            ClientEvents = new CalendarClientEvents();

            SelectionSettings = new CalendarSelectionSettings { Dates = new List<DateTime>() };

            MinDate = new DateTime(1899, 12, 31);
            MaxDate = new DateTime(2100, 1, 1);
            Value = null;

            this.rendererFactory = rendererFactory;
        }

        public IUrlGenerator UrlGenerator
        {
            get;
            private set;
        }

        public DateTime? Value
        { 
            get; 
            set; 
        }

        public DateTime MinDate
        {
            get;
            set;
        }

        public DateTime MaxDate
        {
            get;
            set;
        }

        public string Theme 
        { 
            get; 
            set; 
        }

        public CalendarClientEvents ClientEvents 
        {
            get;
            private set;
        }

        public CalendarSelectionSettings SelectionSettings
        {
            get;
            set;
        }

        public override void WriteInitializationScript(System.IO.TextWriter writer)
        {
            IClientSideObjectWriter objectWriter = ClientSideObjectWriterFactory.Create(Id, "tCalendar", writer);

            objectWriter.Start();

            objectWriter.AppendDateOnly("selectedDate", this.Value);
            objectWriter.AppendDateOnly("minDate", this.MinDate);
            objectWriter.AppendDateOnly("maxDate", this.MaxDate);

            objectWriter.AppendDatesOnly("dates", SelectionSettings.Dates);
            objectWriter.Append("urlFormat", urlFormat);

            objectWriter.Append("onLoad", ClientEvents.OnLoad);
            objectWriter.Append("onChange", ClientEvents.OnChange);

            objectWriter.Complete();

            base.WriteInitializationScript(writer);
        }

        protected override void WriteHtml(System.Web.UI.HtmlTextWriter writer)
        {
            ICalendarHtmlBuilder renderer = rendererFactory.Create(this);

            DefineUrlFormat();
            VerifySettings();

            IHtmlNode rootTag = renderer.Build();

            rootTag.Children.Add(renderer.NavigationTag());

            IHtmlNode contentTag = renderer.ContentTag();
            contentTag.Children.Add(BuildWeekHeader(renderer));
            contentTag.Children.Add(BuildMonthView(renderer));

            rootTag.Children.Add(contentTag);

            rootTag.WriteTo(writer);
        }

        private static IHtmlNode BuildWeekHeader(ICalendarHtmlBuilder renderer) 
        {
            IHtmlNode headerTag = renderer.HeaderTag();
            IHtmlNode row = renderer.RowTag();

            foreach (string day in Culture.Current.DateTimeFormat.DayNames)
            {
                row.Children.Add(renderer.HeaderCellTag(day));
            }

            headerTag.Children.Add(row);

            return headerTag;
        }

        private IHtmlNode BuildMonthView(ICalendarHtmlBuilder renderer) 
        {
            IHtmlNode monthTag = renderer.MonthTag();

            DateTime? focusedDate = this.DetermineFocusedDate();
            int dPosIndex = (int)(new DateTime(focusedDate.Value.Year, focusedDate.Value.Month, 1)).DayOfWeek;

            DateTime prevDate = focusedDate.Value.AddMonths(-1);
            int firstDayOfMonthView = DateTime.DaysInMonth(prevDate.Year, prevDate.Month) - (dPosIndex > 0 ? dPosIndex - 1 : dPosIndex);

            DateTime startDate = new DateTime(prevDate.Year, prevDate.Month, firstDayOfMonthView);
            for (int weekRow = 0; weekRow < 6; weekRow++)
            {
                IHtmlNode rowTag = renderer.RowTag();

                for (int day = 0; day < 7; day++) 
                {
                    renderer.CellTag(startDate, urlFormat, startDate.Month != focusedDate.Value.Month).AppendTo(rowTag);
                    startDate = startDate.AddDays(1);
                }
                monthTag.Children.Add(rowTag);
            }
            return monthTag;
        }

        private void VerifySettings()
        {
            if (MinDate > MaxDate)
            {
                throw new ArgumentException(TextResource.MinDateShouldBeLessThanMaxDate);
            }

            if ((Value != null) && (MinDate > Value || Value > MaxDate))
            {
                throw new ArgumentOutOfRangeException(TextResource.DateOutOfRange);
            }
        }

        private void DefineUrlFormat()
        {
            if (this.SelectionSettings.RouteValues != null)
            {
                KeyValuePair<string, object> value = this.SelectionSettings.RouteValues.First();
                SelectionSettings.RouteValues[value.Key] = "{0}";
                urlFormat = HttpUtility.UrlDecode(SelectionSettings.GenerateUrl(ViewContext, UrlGenerator));
                urlFormat = urlFormat.ToLower();
            }
        }
    }
}
