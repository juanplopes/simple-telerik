// (c) Copyright 2002-2010 Telerik 
// This source is subject to the GNU General Public License, version 2
// See http://www.gnu.org/licenses/gpl-2.0.html. 
// All other rights reserved.

namespace Telerik.Web.Mvc.UI
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Web.UI;
    using System.Web.Mvc;    
    using System.Web.Routing;
    using System.Collections.Generic;

    using Extensions;
    using Infrastructure;
    using Telerik.Web.Mvc.Resources;

    public class TextBoxBase<T> : ViewComponentBase, ITextbox<T> where T : struct
    {
        internal readonly ITextboxBaseHtmlBuilderFactory<T> rendererFactory;

        public TextBoxBase(ViewContext viewContext, IClientSideObjectWriterFactory clientSideObjectWriterFactory, ITextboxBaseHtmlBuilderFactory<T> rendererFactory)
            : base(viewContext, clientSideObjectWriterFactory)
        {
            this.rendererFactory = rendererFactory;

            Spinners = true;

            InputHtmlAttributes = new RouteValueDictionary();

            ClientEvents = new TextboxBaseClientEvents();
        }

        public IDictionary<string, object> InputHtmlAttributes
        {
            get;
            set;
        }

        public string Theme
        {
            get;
            set;
        }

        public T? Value
        {
            get;
            set;
        }

        public T IncrementStep
        {
            get;
            set;
        }

        public T MinValue
        {
            get;
            set;
        }

        public T MaxValue
        {
            get;
            set;
        }

        public int NumberGroupSize
        {
            get;
            set;
        }

        public string NumberGroupSeparator
        {
            get;
            set;
        }

        public int NegativePatternIndex
        {
            get;
            set;
        }

        public string EmptyMessage 
        { 
            get; 
            set; 
        }

        public bool Spinners
        {
            get;
            set;
        }

        public string ButtonTitleUp
        {
            get;
            set;
        }

        public string ButtonTitleDown
        {
            get;
            set;
        }

        public TextboxBaseClientEvents ClientEvents
        {
            get;
            private set;
        }
    }
}
