// (c) Copyright 2002-2010 Telerik 
// This source is subject to the GNU General Public License, version 2
// See http://www.gnu.org/licenses/gpl-2.0.html. 
// All other rights reserved.

namespace Telerik.Web.Mvc.UI
{
    using System;
    using Infrastructure;

    public interface ICalendarHtmlBuilder
    {
        IHtmlNode Build();

        IHtmlNode NavigationTag();

        IHtmlNode ContentTag();

        IHtmlNode HeaderTag();

        IHtmlNode HeaderCellTag(string dayOfWeek);

        IHtmlNode MonthTag();

        IHtmlNode RowTag();

        IHtmlNode CellTag(DateTime day, string urlFormat, bool isOtherMonth);
    }
}
