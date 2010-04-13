// (c) Copyright 2002-2010 Telerik 
// This source is subject to the GNU General Public License, version 2
// See http://www.gnu.org/licenses/gpl-2.0.html. 
// All other rights reserved.

namespace Telerik.Web.Mvc.UI.Fluent
{
    using System.Web.Routing;

    public class TreeViewBindingSettingsBuilder : IHideObjectMembers
    {
        private TreeViewBindingSettings settings;

        public TreeViewBindingSettingsBuilder(TreeViewBindingSettings settings)
        {
            this.settings = settings;
        }

        public TreeViewBindingSettingsBuilder Enabled(bool value)
        {
            settings.Enabled = value;

            return this;
        }

        public TreeViewBindingSettingsBuilder Select(string actionName, string controllerName, RouteValueDictionary routeValues)
        {
            settings.Select.Action(actionName, controllerName, routeValues);

            return this;
        }

        public TreeViewBindingSettingsBuilder Select(string actionName, string controllerName, object routeValues)
        {
            settings.Select.Action(actionName, controllerName, routeValues);

            return this;
        }

        public TreeViewBindingSettingsBuilder Select(string actionName, string controllerName)
        {
            return Select(actionName, controllerName, (object)null);
        }

        public TreeViewBindingSettingsBuilder Select(string routeName, RouteValueDictionary routeValues)
        {
            settings.Select.Route(routeName, routeValues);

            return this;
        }

        public TreeViewBindingSettingsBuilder Select(string routeName, object routeValues)
        {
            settings.Select.Route(routeName, routeValues);

            return this;
        }

        public TreeViewBindingSettingsBuilder Select(string routeName)
        {
            return Select(routeName, (object)null);
        }
    }
}
