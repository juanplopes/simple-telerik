// (c) Copyright 2002-2010 Telerik 
// This source is subject to the GNU General Public License, version 2
// See http://www.gnu.org/licenses/gpl-2.0.html. 
// All other rights reserved.

namespace Telerik.Web.Mvc.UI
{
    public interface IGridBoundColumn : IGridColumn
    {
        bool Encoded
        {
            get;
            set;
        }

        bool Filterable
        {
            get;
            set;
        }

        bool Sortable
        {
            get;
            set;
        }
#if MVC2
        bool ReadOnly
        {
            get;
            set;
        }
#endif
    }
}
