// (c) Copyright 2002-2010 Telerik 
// This source is subject to the GNU General Public License, version 2
// See http://www.gnu.org/licenses/gpl-2.0.html. 
// All other rights reserved.

namespace Telerik.Web.Mvc.UI
{
    using Infrastructure;

    public abstract class GridColumnHtmlBuilderBase<TModel, TColumn> : IGridColumnHtmlBuilder<TModel> where TColumn : GridColumnBase<TModel> where TModel : class
    {
        protected GridColumnHtmlBuilderBase(TColumn column)
        {
            Guard.IsNotNull(column, "column");

            Column = column;
        }

        protected TColumn Column
        {
            get;
            private set;
        }

        public abstract void Html(GridCell<TModel> context, IHtmlNode td);
    }
}