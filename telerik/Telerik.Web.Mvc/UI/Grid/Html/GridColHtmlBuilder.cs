// (c) Copyright 2002-2010 Telerik 
// This source is subject to the GNU General Public License, version 2
// See http://www.gnu.org/licenses/gpl-2.0.html. 
// All other rights reserved.

namespace Telerik.Web.Mvc.UI
{
    using System.Web.Mvc;
    using Telerik.Web.Mvc.Extensions;
    using Telerik.Web.Mvc.Infrastructure;

    public class GridColHtmlBuilder : HtmlBuilderBase
    {
        private readonly IGridColumn column;
        
        public GridColHtmlBuilder(IGridColumn column)
        {
            Guard.IsNotNull(column, "column");
            this.column = column;
        }
        
        protected override IHtmlNode BuildCore()
        {
            var col = new HtmlTag("col", TagRenderMode.SelfClosing)
                .ToggleAttribute("style", "width:" + column.Width, column.Width.HasValue())
                .ToggleAttribute("style", "display:none;", column.Hidden);

            return col;
        }
    }
}
