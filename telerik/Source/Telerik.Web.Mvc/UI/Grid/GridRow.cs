// (c) Copyright 2002-2010 Telerik 
// This source is subject to the GNU General Public License, version 2
// See http://www.gnu.org/licenses/gpl-2.0.html. 
// All other rights reserved.

namespace Telerik.Web.Mvc.UI
{
    using System;
    using System.Collections.Generic;

    public class GridRow<T> where T : class
    {
        public GridRow(T dataItem, int index)
        {
            DataItem = dataItem;
            Index = index;
            IsAlternate = (index % 2) != 0;
            HtmlAttributes = new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase);
        }

        public T DataItem
        {
            get;
            private set;
        }

        public int Index
        {
            get;
            private set;
        }

        public bool IsAlternate
        {
            get;
            private set;
        }

        public IDictionary<string, object> HtmlAttributes
        {
            get;
            private set;
        }

        public bool Selected
        {
            get;
            set;
        }

#if MVC2
        public bool InEditMode
        {
            get;
            set;
        }

        public bool InInsertMode
        {
            get;
            set;
        }
#endif
    }
}