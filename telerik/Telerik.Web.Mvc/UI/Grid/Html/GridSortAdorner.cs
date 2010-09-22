// (c) Copyright 2002-2010 Telerik 
// This source is subject to the GNU General Public License, version 2
// See http://www.gnu.org/licenses/gpl-2.0.html. 
// All other rights reserved.

namespace Telerik.Web.Mvc.UI
{
    using System.ComponentModel;
    using Infrastructure;
    
    public class GridSortAdorner : IHtmlAdorner
    {
        private readonly IGridBoundColumn column;

        public GridSortAdorner(IGridBoundColumn column)
        {
            Guard.IsNotNull(column, "column");

            this.column = column;
        }

        public void ApplyTo(IHtmlNode cell)
        {
            var sortLink = new HtmlTag("a")
                            .AddClass(UIPrimitives.Link)
                            .Attribute("href", column.GetSortUrl());

            var text = new LiteralNode(cell.InnerHtml);
            
            cell.Children.Clear();

            text.AppendTo(sortLink);

            if (column.SortDirection.HasValue)
            {
                var sortArrow = new HtmlTag("span")
                    .AddClass(UIPrimitives.Icon)
                    .ToggleClass("t-arrow-up", column.SortDirection == ListSortDirection.Ascending)
                    .ToggleClass("t-arrow-down", column.SortDirection == ListSortDirection.Descending);
                    
                sortArrow.AppendTo(sortLink);
            }

            cell.Children.Insert(0, sortLink);
        }
    }
}
