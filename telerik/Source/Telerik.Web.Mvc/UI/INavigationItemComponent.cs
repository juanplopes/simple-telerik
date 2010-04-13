// (c) Copyright 2002-2010 Telerik 
// This source is subject to the GNU General Public License, version 2
// See http://www.gnu.org/licenses/gpl-2.0.html. 
// All other rights reserved.

namespace Telerik.Web.Mvc.UI
{
    using Infrastructure;

    using System.Web.Mvc;

    public interface INavigationItemComponent<TItem> : INavigationItemContainer<TItem>
        where TItem: NavigationItem<TItem>
    {
        IUrlGenerator UrlGenerator
        {
            get;
        }

        ViewContext ViewContext
        {
            get;
        }
    }
}
