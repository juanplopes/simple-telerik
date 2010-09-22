// (c) Copyright 2002-2010 Telerik 
// This source is subject to the GNU General Public License, version 2
// See http://www.gnu.org/licenses/gpl-2.0.. 
// All other rights reserved.

namespace Telerik.Web.Mvc.UI
{
    using System;

    public class DatePickerClientEvents
    {
        public DatePickerClientEvents()
        {
            OnLoad = new ClientEvent();
            OnChange = new ClientEvent();
            OnOpen = new ClientEvent();
            OnClose = new ClientEvent();
        }

        public ClientEvent OnLoad { get; private set; }

        public ClientEvent OnChange { get; private set; }

        public ClientEvent OnOpen { get; private set; }

        public ClientEvent OnClose { get; private set; }
    }
}
