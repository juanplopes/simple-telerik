// (c) Copyright 2002-2010 Telerik 
// This source is subject to the GNU General Public License, version 2
// See http://www.gnu.org/licenses/gpl-2.0.html. 
// All other rights reserved.

namespace Telerik.Web.Mvc.UI
{
    using System;

    public class GridColumnHeaderHtmlBuilder<TModel, TColumn> : IGridColumnHeaderHtmlBuilder<TModel> where TColumn : GridColumnBase<TModel> where TModel : class
    {
        public GridColumnHeaderHtmlBuilder(TColumn  column)
        {
            Column = column;
        }

        protected TColumn Column
        {
            get;
            set;
        }

        public virtual void Html(IHtmlNode parent)
        {
            if (!String.IsNullOrEmpty(Column.Title))
            {
                new TextNode(Column.Title).AppendTo(parent);
            }
            else
            {
                parent.Html("&nbsp;");
            }
        }
    }
}