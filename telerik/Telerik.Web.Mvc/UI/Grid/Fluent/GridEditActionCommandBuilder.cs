// (c) Copyright 2002-2010 Telerik 
// This source is subject to the GNU General Public License, version 2
// See http://www.gnu.org/licenses/gpl-2.0.html. 
// All other rights reserved.

namespace Telerik.Web.Mvc.UI.Fluent
{
    using System.Web.Routing;

    public class GridEditActionCommandBuilder<T> : GridActionCommandBuilderBase<T, GridEditActionCommand<T>, GridEditActionCommandBuilder<T>>
        where T : class
    {
        public GridEditActionCommandBuilder(GridEditActionCommand<T> command)
            : base(command)
        {
        }
    }
}
