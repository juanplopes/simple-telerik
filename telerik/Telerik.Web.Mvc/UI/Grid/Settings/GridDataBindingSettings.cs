// (c) Copyright 2002-2010 Telerik 
// This source is subject to the GNU General Public License, version 2
// See http://www.gnu.org/licenses/gpl-2.0.html. 
// All other rights reserved.

namespace Telerik.Web.Mvc.UI
{
    public class GridDataBindingSettings
    {
        private readonly IGrid grid;

        public GridDataBindingSettings(IGrid grid)
        {
            this.grid = grid;
            Server = new GridBindingSettings(grid);
            Ajax = new GridBindingSettings(grid);
            WebService = new GridBindingSettings(grid);
        }

        public GridBindingSettings Server
        {
            get;
            private set;
        }

        public GridBindingSettings Ajax
        {
            get;
            private set;
        }

        public GridBindingSettings WebService
        {
            get;
            private set;
        }
    }
}
