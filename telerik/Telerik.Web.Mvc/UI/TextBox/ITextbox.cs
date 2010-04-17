// (c) Copyright 2002-2010 Telerik 
// This source is subject to the GNU General Public License, version 2
// See http://www.gnu.org/licenses/gpl-2.0.html. 
// All other rights reserved.

namespace Telerik.Web.Mvc.UI
{
    using System.Collections.Generic;

    public interface ITextbox<T>
    {
        IDictionary<string, object> InputHtmlAttributes { get; set; }

        T MinValue { get; set; }
        
        T MaxValue { get; set; }
        
        T IncrementStep { get; set; }
        
        bool Spinners { get; set; }
 
        int NumberGroupSize{ get; set; }

        string NumberGroupSeparator{ get; set; }

        string EmptyMessage{ get; set; }

        string ButtonTitleUp { get; set; }

        string ButtonTitleDown { get; set; }
    }
}
