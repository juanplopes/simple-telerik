// (c) Copyright 2002-2010 Telerik 
// This source is subject to the GNU General Public License, version 2
// See http://www.gnu.org/licenses/gpl-2.0.html. 
// All other rights reserved.

namespace Telerik.Web.Mvc.UI
{
    using System;
    using System.ComponentModel;
    
    public interface IGridBoundColumn : IGridColumn
    {
        string Format
        {
            get;
            set;
        }

        bool Groupable
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

        string Member
        {
            get;
            set;
        }

        Type MemberType
        {
            get;
            set;
        }

        string GetSortUrl();

        ListSortDirection? SortDirection
        {
            get;
        }

#if MVC2
        string EditorHtml
        {
            get;
        }

        bool ReadOnly
        {
            get;
            set;
        }
#endif
    }
}
