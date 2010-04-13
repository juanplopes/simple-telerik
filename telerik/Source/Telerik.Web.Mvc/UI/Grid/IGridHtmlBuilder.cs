using Telerik.Web.Mvc.Infrastructure;
// (c) Copyright 2002-2010 Telerik 
// This source is subject to the GNU General Public License, version 2
// See http://www.gnu.org/licenses/gpl-2.0.html. 
// All other rights reserved.

namespace Telerik.Web.Mvc.UI
{
    using Infrastructure;
    using Infrastructure.Implementation;

    public interface IGridHtmlBuilder<T> where T : class
    {
        IHtmlNode GridTag();

        IHtmlNode HeadTag(IHtmlNode parentTag);

        IHtmlNode TableTag();

        IHtmlNode RowTag();

        IHtmlNode GroupIndicatorTag(GroupDescriptor groupDescriptor);

        IHtmlNode EmptyRowTag();

        IHtmlNode GroupRowTag(IGroup group, int level);

        IHtmlNode RowTag(GridRow<T> context);

        IHtmlNode EditFormTag(IHtmlNode tr, INavigatable navigatable);

        IHtmlNode CellTag(GridCell<T> context);

        IHtmlNode BodyTag(IHtmlNode parentTag);

        IHtmlNode HeadCellTag(GridColumnBase<T> column);

        IHtmlNode FootTag(IHtmlNode parentTag);

        IHtmlNode FootCellTag();

        IHtmlNode LoadingIndicatorTag();

        IHtmlNode PagerTag();

        IHtmlNode PagerStatusTag();
    }
}