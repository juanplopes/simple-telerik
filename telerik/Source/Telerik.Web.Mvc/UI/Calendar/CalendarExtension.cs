// (c) Copyright 2002-2010 Telerik 
// This source is subject to the GNU General Public License, version 2
// See http://www.gnu.org/licenses/gpl-2.0.html. 
// All other rights reserved.

namespace Telerik.Web.Mvc.UI
{
    using System;

    public static class CalendarExtension
    {
        public static bool IsDateInRange(this Calendar calendar, DateTime date)
        {
            return calendar.MinDate <= date && date <= calendar.MaxDate;
        }

        public static DateTime? DetermineFocusedDate(this Calendar calendar)
        {
            DateTime? focusedDate = calendar.Value;
            if (focusedDate == null)
            {
                focusedDate = calendar.IsDateInRange(DateTime.Today) ? DateTime.Today : calendar.MinDate;
            }
            return focusedDate;
        }
    }
}
