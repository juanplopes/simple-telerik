// (c) Copyright 2002-2010 Telerik 
// This source is subject to the GNU General Public License, version 2
// See http://www.gnu.org/licenses/gpl-2.0.html. 
// All other rights reserved.

namespace Telerik.Web.Mvc.UI
{
    using System.Web.Routing;
    
    public abstract class GridToolBarCommandBuilderBase<TModel, TCommand, TBuilder> : IHideObjectMembers
        where TModel : class
        where TCommand : GridToolBarCommandBase<TModel>
        where TBuilder : GridToolBarCommandBuilderBase<TModel, TCommand, TBuilder>
    {
        protected GridToolBarCommandBuilderBase(TCommand command)
        {
            Command = command;
        }

        public TBuilder HtmlAttributes(object values)
        {
            Command.HtmlAttributes = new RouteValueDictionary(values);

            return this as TBuilder;
        }

        protected TCommand Command
        {
            get;
            private set;
        }
    }
}