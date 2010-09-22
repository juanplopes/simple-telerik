// (c) Copyright 2002-2010 Telerik 
// This source is subject to the GNU General Public License, version 2
// See http://www.gnu.org/licenses/gpl-2.0.html. 
// All other rights reserved.

namespace Telerik.Web.Mvc.UI
{
    using Telerik.Web.Mvc.Infrastructure;

    public class GridHeaderHierarchyAdorner : IHtmlAdorner
    {
        public void ApplyTo(IHtmlNode target)
        {
            var hierarchyCell = new HtmlTag("th")
                .AddClass(UIPrimitives.Header, UIPrimitives.Grid.HierarchyCell);
            
            target.Children.Insert(0, hierarchyCell);
        }
    }
}
