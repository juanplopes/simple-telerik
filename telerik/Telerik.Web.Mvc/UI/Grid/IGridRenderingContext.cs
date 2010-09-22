// (c) Copyright 2002-2010 Telerik 
// This source is subject to the GNU General Public License, version 2
// See http://www.gnu.org/licenses/gpl-2.0.html. 
// All other rights reserved.
namespace Telerik.Web.Mvc.UI
{
    public interface IGridRenderingContext<T>
        where T : class
    {
        Grid<T> Grid 
        { 
            get; 
        }
#if MVC2        
        bool InEditMode 
        { 
            get; 
            set; 
        }
        
        bool InInsertMode 
        { 
            get; 
            set; 
        }
#endif
        T DataItem 
        { 
            get; 
        }
    }
}
