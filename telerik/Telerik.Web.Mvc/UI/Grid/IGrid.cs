// (c) Copyright 2002-2010 Telerik 
// This source is subject to the GNU General Public License, version 2
// See http://www.gnu.org/licenses/gpl-2.0.html. 
// All other rights reserved.
namespace Telerik.Web.Mvc.UI
{
    using System.Collections.Generic;
    using System.Web.Mvc;

    public interface IGrid : IGridBindingContext
    {
        bool HasDetailView
        {
            get;
        }
        
        GridSortSettings Sorting
        {
            get;
        }

        bool IsSelfInitialized
        {
            get;
        }

#if MVC2
        string EditorHtml 
        { 
            get; 
        }
#endif
        GridResizingSettings Resizing
        {
            get;
        }

        GridDataProcessor DataProcessor 
        { 
            get;
        }

        GridFilteringSettings Filtering
        {
            get;
        }

        GridGroupingSettings Grouping
        {
            get;
        }

        GridEditingSettings Editing 
        { 
            get; 
        }

        bool IsClientBinding
        {
            get;
        }

        IUrlGenerator UrlGenerator
        {
            get;
        }

        ViewContext ViewContext
        {
            get;
        }

        IEnumerable<IGridColumn> Columns
        {
            get;
        }

        IEnumerable<IGridDataKey> DataKeys
        {
            get;
        }

        bool IsEmpty
        {
            get;
        }
    }
}
