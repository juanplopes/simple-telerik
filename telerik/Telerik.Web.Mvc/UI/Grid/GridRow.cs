// (c) Copyright 2002-2010 Telerik 
// This source is subject to the GNU General Public License, version 2
// See http://www.gnu.org/licenses/gpl-2.0.html. 
// All other rights reserved.

namespace Telerik.Web.Mvc.UI
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Telerik.Web.Mvc.Extensions;
    using Telerik.Web.Mvc.UI;
    using Telerik.Web.Mvc.UI.Html;

    public class GridRow<T> : IGridRenderingContext<T>
        where T : class
    {
        public GridRow(Grid<T> grid, T dataItem, int index)
        {
            Grid = grid;
            DataItem = dataItem;
            Index = index;
            IsAlternate = (index % 2) != 0;
            HtmlAttributes = new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase);
        }

        public Grid<T> Grid
        {
            get;
            private set;
        }

        public GridDetailRow<T> DetailRow
        {
            get;
            set;
        }

        public T DataItem
        {
            get;
            set;
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
        public GridCell<T> CreateCellFor(GridColumnBase<T> column)
        {
            var cell = new GridCell<T>(column, DataItem)
            {
#if MVC2
                InEditMode = InEditMode,
                InInsertMode = InInsertMode,
#endif
                Selected = Selected
            };

            if (column.Template != null)
            {
                cell.Template.CodeBlockTemplate = column.Template;
            }
            
            if (column.InlineTemplate != null)
            {
                cell.Template.InlineTemplate = column.InlineTemplate;
            }

            return cell;
        }

    }
}