// (c) Copyright 2002-2010 Telerik 
// This source is subject to the GNU General Public License, version 2
// See http://www.gnu.org/licenses/gpl-2.0.html. 
// All other rights reserved.

namespace Telerik.Web.Mvc.UI.Fluent
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class GridActionCommandFactory<T> : IHideObjectMembers where T : class
    {
        public GridActionCommandFactory(GridActionColumn<T> column)
        {
            Column = column;
        }
        
        private GridActionColumn<T> Column
        {
            get;
            set;
        }

        public GridEditActionCommandBuilder<T> Edit()
        {
            GridEditActionCommand<T> command = new GridEditActionCommand<T>();

            Column.Commands.Add(command);

            Column.Grid.Editing.Enabled = true;

            return new GridEditActionCommandBuilder<T>(command);
        }

        public GridDeleteActionCommandBuilder<T> Delete()
        {
            GridDeleteActionCommand<T> command = new GridDeleteActionCommand<T>();

            Column.Commands.Add(command);

            return new GridDeleteActionCommandBuilder<T>(command);
        }
        
        public GridSelectActionCommandBuilder<T> Select()
        {
            GridSelectActionCommand<T> command = new GridSelectActionCommand<T>();

            Column.Commands.Add(command);

            return new GridSelectActionCommandBuilder<T>(command);
        }
    }
}