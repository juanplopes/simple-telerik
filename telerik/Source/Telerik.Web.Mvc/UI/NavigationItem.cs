// (c) Copyright 2002-2010 Telerik 
// This source is subject to the GNU General Public License, version 2
// See http://www.gnu.org/licenses/gpl-2.0.html. 
// All other rights reserved.

namespace Telerik.Web.Mvc.UI
{
    using System;
    using System.Web.Routing;
    using System.Collections.Generic;

    using Infrastructure;

    public abstract class NavigationItem<T> : LinkedObjectBase<T>, INavigatable, IHideObjectMembers, IHtmlAttributesContainer, IContentContainer where T : NavigationItem<T>
    {
        private string text;
        private string routeName;
        private string controllerName;
        private string actionName;
        private string url;

        private bool selected;
        private bool enabled;

        protected NavigationItem()
        {
            HtmlAttributes = new RouteValueDictionary();
            ImageHtmlAttributes = new RouteValueDictionary();
            LinkHtmlAttributes = new RouteValueDictionary();
            RouteValues = new RouteValueDictionary();
            ContentHtmlAttributes = new RouteValueDictionary();
            Visible = true;
            Enabled = true;
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
                controllerName = actionName = url = null;
            }
        }

        public IDictionary<string, object> HtmlAttributes
        {
            get;
            private set;
        }

        public IDictionary<string, object> ImageHtmlAttributes
        {
            get;
            private set;
        }

        public IDictionary<string, object> LinkHtmlAttributes
        {
            get;
            private set;
        }

        public string Text
        {
            get
            {
                return text;
            }
            set
            {
                Guard.IsNotNullOrEmpty(value, "value");

                text = value;
            }
        }

        public bool Visible
        {
            get;
            set;
        }

        public string ImageUrl
        {
            get;
            set;
        }

        public string SpriteCssClasses 
        { 
            get; 
            set; 
        }

        public bool Selected
        {
            get
            {
                return selected;
            }
            set
            {
                selected = value;

                if (selected)
                {
                    enabled = true;
                }
            }
        }

        public bool Enabled
        {
            get
            {
                return enabled;
            }
            set
            {
                enabled = value;

                if (!enabled)
                {
                    selected = false;
                }
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

                routeName = url = null;
            }
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

                routeName = url = null;
            }
        }

        public RouteValueDictionary RouteValues
        {
            get;
            set;
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1056:UriPropertiesShouldNotBeStrings", Justification = "Url might not be a valid uri.")]
        public string Url
        {
            get
            {
                return url;
            }
            set
            {
                Guard.IsNotNullOrEmpty(value, "value");

                url = value;

                routeName = controllerName = actionName = null;
                RouteValues.Clear();
            }
        }

        public IDictionary<string, object> ContentHtmlAttributes
        {
            get;
            private set;
        }

        public Action Content
        {
            get;
            set;
        }
    }
}