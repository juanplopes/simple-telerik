// (c) Copyright 2002-2010 Telerik 
// This source is subject to the GNU General Public License, version 2
// See http://www.gnu.org/licenses/gpl-2.0.html. 
// All other rights reserved.

namespace Telerik.Web.Mvc.UI
{
    using System.Collections.Generic;
    using System.Web.Routing;
    
    public abstract class GridActionCommandBase<T> where T : class
    {
        public abstract string Name { get; }
        
        public IDictionary<string, object> HtmlAttributes
        {
            get;
            private set;
        }
        
        public GridActionCommandBase()
        {
            HtmlAttributes = new RouteValueDictionary();
        }

        public void Html(GridCell<T> context, IHtmlNode parent)
        {
            #if MVC2

            if (context.InInsertMode)
            {
                InsertModeHtml(parent, context);
            }
            else if (context.InEditMode)
            {
                EditModeHtml(parent, context);
            }
            else
            {
                BoundModeHtml(parent, context);
            }

            #endif
        }

        public abstract void EditModeHtml(IHtmlNode parent, GridCell<T> context);

        public abstract void InsertModeHtml(IHtmlNode parent, GridCell<T> context);

        public abstract void BoundModeHtml(IHtmlNode parent, GridCell<T> context);
    }
}