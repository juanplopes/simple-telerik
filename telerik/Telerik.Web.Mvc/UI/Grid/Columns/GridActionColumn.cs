// (c) Copyright 2002-2010 Telerik 
// This source is subject to the GNU General Public License, version 2
// See http://www.gnu.org/licenses/gpl-2.0.html. 
// All other rights reserved.

namespace Telerik.Web.Mvc.UI
{
    using System.Collections.Generic;
    using Telerik.Web.Mvc.UI.Html;

    public class GridActionColumn<T> : GridColumnBase<T>, IGridActionColumn where T : class
    {
        public GridActionColumn(Grid<T> grid) : base(grid)
        {
            Commands = new List<GridActionCommandBase>();
        }

        public IList<GridActionCommandBase> Commands
        {
            get;
            private set;
        }

        public override IGridColumnSerializer CreateSerializer()
        {
            return new GridActionColumnSerializer(this);
        }

        protected override IHtmlBuilder CreateDisplayHtmlBuilderCore(GridCell<T> cell)
        {
            return new GridActionCellHtmlBuilder<T>(cell);
        }
    }
}