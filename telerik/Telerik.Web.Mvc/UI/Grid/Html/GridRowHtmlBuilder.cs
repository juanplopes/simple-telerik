// (c) Copyright 2002-2010 Telerik 
// This source is subject to the GNU General Public License, version 2
// See http://www.gnu.org/licenses/gpl-2.0.html. 
// All other rights reserved.
namespace Telerik.Web.Mvc.UI.Html
{
    using Telerik.Web.Mvc.Extensions;
    using Telerik.Web.Mvc.Infrastructure;
    using Telerik.Web.Mvc.UI;
    
    public class GridRowHtmlBuilder<T> : HtmlBuilderBase
        where T : class
    {
        public GridRow<T> Row
        {
            get;
            private set;
        } 

        public GridRowHtmlBuilder(GridRow<T> row)
        {
            Row = row;
        }
        
        protected override IHtmlNode BuildCore()
        {
            IHtmlNode tr = CreateRow();

            AppendCellsTo(tr);

            return tr;
        }

        private void AppendCellsTo(IHtmlNode tr)
        {
            Row.Grid
               .VisibleColumns
               .Each(column => column
                   .CreateDisplayHtmlBuilder(Row.CreateCellFor(column))
                   .Build()
                   .AppendTo(tr)
               );
        }
        
        protected IHtmlNode CreateRow()
        {
            IHtmlNode tr = new HtmlTag("tr")
                            .Attributes(Row.HtmlAttributes)
                            .ToggleClass("t-alt", Row.IsAlternate)
                            .ToggleClass(UIPrimitives.SelectedState, Row.Selected);

            return tr;
        }
    }
}
