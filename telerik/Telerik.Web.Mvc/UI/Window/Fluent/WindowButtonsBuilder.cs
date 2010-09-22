// (c) Copyright 2002-2010 Telerik 
// This source is subject to the GNU General Public License, version 2
// See http://www.gnu.org/licenses/gpl-2.0.html. 
// All other rights reserved.

namespace Telerik.Web.Mvc.UI.Fluent
{
    using Infrastructure;

    public class WindowButtonsBuilder
    {
        private readonly IWindowButtonsContainer container;

        public WindowButtonsBuilder(IWindowButtonsContainer container)
        {
            Guard.IsNotNull(container, "container");

            this.container = container;
        }

        public WindowButtonsBuilder Close() 
        {
            container.Container.Add(new HeaderButton { Name = "Close", CssClass = "t-close" });

            return this;
        }

        public WindowButtonsBuilder Maximize()
        {
            container.Container.Add(new HeaderButton { Name = "Maximize", CssClass = "t-maximize" });

            return this;
        }

        public WindowButtonsBuilder Refresh()
        {
            container.Container.Add(new HeaderButton { Name = "Refresh", CssClass = "t-refresh" });

            return this;
        }
    }
}
