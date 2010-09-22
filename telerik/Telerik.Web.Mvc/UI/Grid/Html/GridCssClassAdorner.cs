// (c) Copyright 2002-2010 Telerik 
// This source is subject to the GNU General Public License, version 2
// See http://www.gnu.org/licenses/gpl-2.0.html. 
// All other rights reserved.

namespace Telerik.Web.Mvc.UI.Html
{
    using System.Collections.Generic;
    using System.Linq;

    public class GridCssClassAdorner : IHtmlAdorner
    {
        public GridCssClassAdorner()
        {
            CssClasses = new List<string>();
        }

        public IList<string> CssClasses
        {
            get;
            private set;
        }
        
        public void ApplyTo(IHtmlNode target)
        {
            target.AddClass(CssClasses.ToArray());
        }
    }
}
