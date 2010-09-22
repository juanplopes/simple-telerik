// (c) Copyright 2002-2010 Telerik 
// This source is subject to the GNU General Public License, version 2
// See http://www.gnu.org/licenses/gpl-2.0.html. 
// All other rights reserved.

namespace Telerik.Web.Mvc.UI
{
    using System.Collections.Generic;
    using System.Web.Routing;
    
    public abstract class GridActionCommandBase
    {
        public abstract string Name { get; }

        public GridButtonType ButtonType
        {
            get;
            set;
        }

        public IDictionary<string, object> HtmlAttributes
        {
            get;
            private set;
        }

        public IDictionary<string, object> ImageHtmlAttributes
        {
            get;
            private set;
        }

        public GridActionCommandBase()
        {
            ButtonType = GridButtonType.Text;
            HtmlAttributes = new RouteValueDictionary();
            ImageHtmlAttributes = new RouteValueDictionary();
        }

        public void Html<T>(IGridRenderingContext<T> context, IHtmlNode parent)
             where T : class
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

        public abstract void EditModeHtml<T>(IHtmlNode parent, IGridRenderingContext<T> context) where T : class;

        public abstract void InsertModeHtml<T>(IHtmlNode parent, IGridRenderingContext<T> context) where T : class;

        public abstract void BoundModeHtml<T>(IHtmlNode parent, IGridRenderingContext<T> context) where T : class;
    }
}