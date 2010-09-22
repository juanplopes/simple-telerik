// (c) Copyright 2002-2010 Telerik 
// This source is subject to the GNU General Public License, version 2
// See http://www.gnu.org/licenses/gpl-2.0.html. 
// All other rights reserved.
namespace Telerik.Web.Mvc.UI.Html
{
    using Telerik.Web.Mvc.Extensions;

    public class GridActionCellHtmlBuilder<T> : GridDataCellHtmlBuilder<T>
        where T : class
    {
        public GridActionCellHtmlBuilder(GridCell<T> cell)
            : base(cell)
        {
        }

        protected override IHtmlNode BuildCore()
        {
            var td = CreateCell();
            var commands = ((GridActionColumn<T>)Cell.Column).Commands;

#if MVC2
            if (Cell.Grid.Editing.Mode != GridEditMode.PopUp)
            {
                if (Cell.InEditMode)
                {
                    commands.Each(command => command.EditModeHtml<T>(td, Cell));
                    return td;
                }
                else if (Cell.InInsertMode)
                {
                    commands.Each(command => command.InsertModeHtml<T>(td, Cell));
                    return td;
                }
            }
#endif
            commands.Each(command => command.BoundModeHtml<T>(td, Cell));

            return td;
        }
    }
}
