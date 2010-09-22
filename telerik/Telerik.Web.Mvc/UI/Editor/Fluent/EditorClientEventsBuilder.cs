// (c) Copyright 2002-2010 Telerik 
// This source is subject to the GNU General Public License, version 2
// See http://www.gnu.org/licenses/gpl-2.0.html. 
// All other rights reserved.

namespace Telerik.Web.Mvc.UI.Fluent
{
    using System;
    using Telerik.Web.Mvc.Infrastructure;
    
    public class EditorClientEventsBuilder : IHideObjectMembers
    {
        private EditorClientEvents events;

        public EditorClientEventsBuilder(EditorClientEvents events)
        {
            this.events = events;
        }

        public EditorClientEventsBuilder OnLoad(string onLoadHandlerName)
        {
            return HandlerName(events.OnLoad, onLoadHandlerName);
        }
        
        public EditorClientEventsBuilder OnLoad(Action inlineCode)
        {
            return InlineCode(events.OnLoad, inlineCode);
        }
        
        public EditorClientEventsBuilder OnExecute(string onLoadHandlerName)
        {
            return HandlerName(events.OnExecute, onLoadHandlerName);
        }

        public EditorClientEventsBuilder OnExecute(Action inlineCode)
        {
            return InlineCode(events.OnExecute, inlineCode);
        }
        
        public EditorClientEventsBuilder OnSelectionChange(string onLoadHandlerName)
        {
            return HandlerName(events.OnSelectionChange, onLoadHandlerName);
        }

        public EditorClientEventsBuilder OnSelectionChange(Action inlineCode)
        {
            return InlineCode(events.OnSelectionChange, inlineCode);
        }

        public EditorClientEventsBuilder OnChange(string onLoadHandlerName)
        {
            return HandlerName(events.OnChange, onLoadHandlerName);
        }

        public EditorClientEventsBuilder OnChange(Action inlineCode)
        {
            return InlineCode(events.OnChange, inlineCode);
        }

        private EditorClientEventsBuilder InlineCode(ClientEvent e, Action code)
        {
            Guard.IsNotNull(code, "code");

            e.InlineCode = code;

            return this;
        }
        
        private EditorClientEventsBuilder HandlerName(ClientEvent e, string handler)
        {
            Guard.IsNotNullOrEmpty(handler, "handler");

            e.HandlerName = handler;
            
            return this;
        }
    }
}
