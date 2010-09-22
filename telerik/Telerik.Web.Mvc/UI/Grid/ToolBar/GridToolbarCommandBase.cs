// (c) Copyright 2002-2010 Telerik 
// This source is subject to the GNU General Public License, version 2
// See http://www.gnu.org/licenses/gpl-2.0.html. 
// All other rights reserved.

namespace Telerik.Web.Mvc.UI
{
    using System.Collections.Generic;
    using System.Web.Routing;
    
    public abstract class GridToolBarCommandBase<T> where T : class
    {
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
        
        public GridToolBarCommandBase()
        {
            ButtonType = GridButtonType.Text;
            HtmlAttributes = new RouteValueDictionary();
            ImageHtmlAttributes = new RouteValueDictionary();
        }
        
        public abstract void Html(Grid<T> context, IHtmlNode parent);
    }
}