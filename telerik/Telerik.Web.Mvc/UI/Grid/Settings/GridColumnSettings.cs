// (c) Copyright 2002-2010 Telerik 
// This source is subject to the GNU General Public License, version 2
// See http://www.gnu.org/licenses/gpl-2.0.html. 
// All other rights reserved.

namespace Telerik.Web.Mvc.UI
{
    using System;
    using System.Collections.Generic;
    using System.Web;
    using System.Web.Routing;
    using Telerik.Web.Mvc.Extensions;

    public class GridColumnSettings<T> : GridColumnSettings
        where T : class
    {
        public Action<T> Template
        {
            get;
            set;
        }
    }
    
    public class GridColumnSettings
    {
        private string member;
        private string clientTemplate;

        public GridColumnSettings()
        {
            Sortable = true;
            Encoded = true;
            Filterable = true;
            Groupable = true;
            Visible = true;
            HeaderHtmlAttributes = new RouteValueDictionary();
            HtmlAttributes = new RouteValueDictionary();
        }

        public string ClientTemplate
        {
            get
            {
                return clientTemplate;
            }
            set
            {
                clientTemplate = HttpUtility.HtmlDecode(value);
            }
        }

        public bool Encoded
        {
            get;
            set;
        }

        public bool Filterable
        {
            get;
            set;
        }

        public string Format
        {
            get;
            set;
        }

        public bool Groupable
        {
            get;
            set;
        }

        public IDictionary<string, object> HeaderHtmlAttributes
        {
            get;
            private set;
        }

        public bool Hidden
        {
            get;
            set;
        }

        public IDictionary<string, object> HtmlAttributes
        {
            get;
            private set;
        }

        public string Member
        {
            get
            {
                return member;
            }
            set
            {
                member = value;

                if (!Title.HasValue())
                {
                    Title = member.AsTitle();
                }
            }
        }

        public bool ReadOnly
        {
            get;
            set;
        }

        public bool Sortable
        {
            get;
            set;
        }

        public string Title
        {
            get;
            set;
        }

        public bool Visible
        {
            get;
            set;
        }

        public string Width
        {
            get;
            set;
        }
    }
}