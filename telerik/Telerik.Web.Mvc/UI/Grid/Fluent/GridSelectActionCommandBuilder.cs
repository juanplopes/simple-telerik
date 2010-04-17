// (c) Copyright 2002-2010 Telerik 
// This source is subject to the GNU General Public License, version 2
// See http://www.gnu.org/licenses/gpl-2.0.html. 
// All other rights reserved.

namespace Telerik.Web.Mvc.UI.Fluent
{
    public class GridSelectActionCommandBuilder<T> : GridActionCommandBuilderBase<T, GridSelectActionCommand<T>, GridSelectActionCommandBuilder<T>>
        where T : class
    {
        public GridSelectActionCommandBuilder(GridSelectActionCommand<T> command)
            : base(command)
        {
        }
    }
}
