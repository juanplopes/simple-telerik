// (c) Copyright 2002-2010 Telerik 
// This source is subject to the GNU General Public License, version 2
// See http://www.gnu.org/licenses/gpl-2.0.html. 
// All other rights reserved.

namespace Telerik.Web.Mvc.UI.Fluent
{
    using System;
    using System.Linq.Expressions;
    using System.Web.Mvc;
    using System.Web.Routing;

    public class GridBindingSettingsBuilder : IHideObjectMembers
    {
        private GridBindingSettings settings;

        public GridBindingSettingsBuilder(GridBindingSettings settings)
        {
            this.settings = settings;
        }
        
        public GridBindingSettingsBuilder Enabled(bool value)
        {
            settings.Enabled = value;

            return this;
        }

        public GridBindingSettingsBuilder Select(string actionName, string controllerName, RouteValueDictionary routeValues)
        {
            settings.Select.Action(actionName, controllerName, routeValues);

            return this;
        }

        public GridBindingSettingsBuilder Select(string actionName, string controllerName, object routeValues)
        {
            settings.Select.Action(actionName, controllerName, routeValues);

            return this;
        }

        public GridBindingSettingsBuilder Select(string actionName, string controllerName)
        {
            return Select(actionName, controllerName, (object)null);
        }

        public GridBindingSettingsBuilder Select(string routeName, RouteValueDictionary routeValues)
        {
            settings.Select.Route(routeName, routeValues);

            return this;
        }

        public GridBindingSettingsBuilder Select(string routeName, object routeValues)
        {
            settings.Select.Route(routeName, routeValues);

            return this;
        }

        public GridBindingSettingsBuilder Select(string routeName)
        {
            return Select(routeName, (object)null);
        }

        public GridBindingSettingsBuilder Select<TController>(Expression<Action<TController>> controllerAction) where TController : Controller
        {
            settings.Select.Action(controllerAction);

            return this;
        }
        
        public GridBindingSettingsBuilder Insert(string actionName, string controllerName, RouteValueDictionary routeValues)
        {
            settings.Insert.Action(actionName, controllerName, routeValues);

            return this;
        }

        public GridBindingSettingsBuilder Insert(string actionName, string controllerName, object routeValues)
        {
            settings.Insert.Action(actionName, controllerName, routeValues);

            return this;
        }

        public GridBindingSettingsBuilder Insert(string actionName, string controllerName)
        {
            return Insert(actionName, controllerName, (object)null);
        }

        public GridBindingSettingsBuilder Insert(string routeName, RouteValueDictionary routeValues)
        {
            settings.Insert.Route(routeName, routeValues);

            return this;
        }

        public GridBindingSettingsBuilder Insert(string routeName, object routeValues)
        {
            settings.Insert.Route(routeName, routeValues);

            return this;
        }

        public GridBindingSettingsBuilder Insert(string routeName)
        {
            return Insert(routeName, (object)null);
        }

        public GridBindingSettingsBuilder Insert<TController>(Expression<Action<TController>> controllerAction) where TController : Controller
        {
            settings.Insert.Action(controllerAction);

            return this;
        }
        
        public GridBindingSettingsBuilder Update(string actionName, string controllerName, RouteValueDictionary routeValues)
        {
            settings.Update.Action(actionName, controllerName, routeValues);

            return this;
        }

        public GridBindingSettingsBuilder Update(string actionName, string controllerName, object routeValues)
        {
            settings.Update.Action(actionName, controllerName, routeValues);

            return this;
        }

        public GridBindingSettingsBuilder Update(string actionName, string controllerName)
        {
            return Update(actionName, controllerName, (object)null);
        }

        public GridBindingSettingsBuilder Update(string routeName, RouteValueDictionary routeValues)
        {
            settings.Update.Route(routeName, routeValues);

            return this;
        }

        public GridBindingSettingsBuilder Update(string routeName, object routeValues)
        {
            settings.Update.Route(routeName, routeValues);

            return this;
        }

        public GridBindingSettingsBuilder Update(string routeName)
        {
            return Update(routeName, (object)null);
        }

        public GridBindingSettingsBuilder Update<TController>(Expression<Action<TController>> controllerAction) where TController : Controller
        {
            settings.Update.Action(controllerAction);

            return this;
        }
        public GridBindingSettingsBuilder Delete(string actionName, string controllerName, RouteValueDictionary routeValues)
        {
            settings.Delete.Action(actionName, controllerName, routeValues);

            return this;
        }

        public GridBindingSettingsBuilder Delete(string actionName, string controllerName, object routeValues)
        {
            settings.Delete.Action(actionName, controllerName, routeValues);

            return this;
        }

        public GridBindingSettingsBuilder Delete(string actionName, string controllerName)
        {
            return Delete(actionName, controllerName, (object)null);
        }

        public GridBindingSettingsBuilder Delete(string routeName, RouteValueDictionary routeValues)
        {
            settings.Delete.Route(routeName, routeValues);

            return this;
        }

        public GridBindingSettingsBuilder Delete(string routeName, object routeValues)
        {
            settings.Delete.Route(routeName, routeValues);

            return this;
        }

        public GridBindingSettingsBuilder Delete(string routeName)
        {
            return Delete(routeName, (object)null);
        }

        public GridBindingSettingsBuilder Delete<TController>(Expression<Action<TController>> controllerAction) where TController : Controller
        {
            settings.Delete.Action(controllerAction);

            return this;
        }
    }
}
