// (c) Copyright 2002-2010 Telerik 
// This source is subject to the GNU General Public License, version 2
// See http://www.gnu.org/licenses/gpl-2.0.html. 
// All other rights reserved.

namespace Telerik.Web.Mvc.UI
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class GridBindingSettings
    {
        public GridBindingSettings()
        {
            Select = new GridRequestSettings();
            Insert = new GridRequestSettings();
            Update = new GridRequestSettings();
            Delete = new GridRequestSettings();
        }

        public bool Enabled
        {
            get;
            set;
        }

        public GridRequestSettings Select
        {
            get;
            private set;
        }

        public GridRequestSettings Insert
        {
            get;
            private set;
        }

        public GridRequestSettings Update
        {
            get;
            private set;
        }

        public GridRequestSettings Delete
        {
            get;
            private set;
        }
    }
}
