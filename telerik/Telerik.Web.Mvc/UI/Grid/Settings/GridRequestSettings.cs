// (c) Copyright 2002-2010 Telerik 
// This source is subject to the GNU General Public License, version 2
// See http://www.gnu.org/licenses/gpl-2.0.html. 
// All other rights reserved.

namespace Telerik.Web.Mvc.UI
{
    using System.Diagnostics.CodeAnalysis;
    using System.Web.Routing;
    
    using Extensions;
    using Infrastructure;

    public class GridRequestSettings : INavigatable
    {
        private string routeName;
        private string controllerName;
        private string actionName;

        public GridRequestSettings()
        {
            RouteValues = new RouteValueDictionary();
        }

        public bool HasValue()
        {
            return (ActionName.HasValue() && ControllerName.HasValue()) || RouteName.HasValue() || Url.HasValue();
        }

        public string ActionName
        {
            get
            {
                return actionName;
            }
            set
            {
                Guard.IsNotNullOrEmpty(value, "value");

                actionName = value;

                routeName = null;
            }
        }

        public string ControllerName
        {
            get
            {
                return controllerName;
            }
            set
            {
                Guard.IsNotNullOrEmpty(value, "value");

                controllerName = value;

                routeName = null;
            }
        }

        public RouteValueDictionary RouteValues
        {
            get;
            private set;
        }
        
        public string RouteName
        {
            get
            {
                return routeName;
            }
            set
            {
                Guard.IsNotNullOrEmpty(value, "value");

                routeName = value;
                controllerName = actionName = null;
            }
        }

        [SuppressMessage("Microsoft.Design", "CA1056:UriPropertiesShouldNotBeStrings")]
        public string Url
        {
            get;
            set;
        }
    }
}