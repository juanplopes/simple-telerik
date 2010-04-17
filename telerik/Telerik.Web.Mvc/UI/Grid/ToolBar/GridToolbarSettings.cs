// (c) Copyright 2002-2010 Telerik 
// This source is subject to the GNU General Public License, version 2
// See http://www.gnu.org/licenses/gpl-2.0.html. 
// All other rights reserved.

namespace Telerik.Web.Mvc.UI
{
    using System.Collections.Generic;

    using Extensions;

    public class GridToolBarSettings<T> where T : class
    {
        public GridToolBarSettings()
        {
            Commands = new List<GridToolBarCommandBase<T>>();
        }

        public bool Enabled
        {
            get
            {
                return !Commands.IsEmpty();
            }
        }

        public IList<GridToolBarCommandBase<T>> Commands
        {
            get;
            private set;
        }
    }
}