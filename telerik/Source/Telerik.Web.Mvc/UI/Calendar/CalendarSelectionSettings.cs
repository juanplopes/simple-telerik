// (c) Copyright 2002-2010 Telerik 
// This source is subject to the GNU General Public License, version 2
// See http://www.gnu.org/licenses/gpl-2.0.html. 
// All other rights reserved.

namespace Telerik.Web.Mvc.UI
{
    using System;
    using System.Collections.Generic;

    public class CalendarSelectionSettings : INavigatable
    {
        public IList<DateTime> Dates { get; set; }

        public string ControllerName
        {
            get;
            set;
        }

        public string ActionName
        {
            get;
            set;
        }

        public System.Web.Routing.RouteValueDictionary RouteValues
        {
            get;
            set;
        }

        //not used in this case
        public string Url { get; set; }
        public string RouteName { get; set; }
    }
}
