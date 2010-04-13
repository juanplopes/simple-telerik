// (c) Copyright 2002-2010 Telerik 
// This source is subject to the GNU General Public License, version 2
// See http://www.gnu.org/licenses/gpl-2.0.html. 
// All other rights reserved.

namespace Telerik.Web.Mvc.UI
{
    using System;
    using System.Collections.Generic;

    public class GridCell<T>
        where T : class
    {
        public GridCell(GridColumnBase<T> column, T dataItem)
        {
            Column = column;
            DataItem = dataItem;
            HtmlAttributes = new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase);
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

        public T DataItem
        {
            get;
            private set;
        }

        public GridColumnBase<T> Column
        {
            get;
            private set;
        }

        public string Text
        {
            get;
            set;
        }

        public Action<T> Content
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