// (c) Copyright 2002-2010 Telerik 
// This source is subject to the GNU General Public License, version 2
// See http://www.gnu.org/licenses/gpl-2.0.html. 
// All other rights reserved.
namespace Telerik.Web.Mvc.UI.Fluent
{
    using System.Web.Routing;
    
    using Extensions;

    public abstract class GridActionCommandBuilderBase<TModel, TCommand, TBuilder>  : IHideObjectMembers
        where TModel : class
        where TCommand : GridActionCommandBase<TModel>
        where TBuilder : GridActionCommandBuilderBase<TModel, TCommand, TBuilder>
    {
        public GridActionCommandBuilderBase(TCommand command)
        {
            Command = command;
        }

        public TBuilder HtmlAttributes(object values)
        {
            Command.HtmlAttributes.Merge(values);
            
            return this as TBuilder;
        }

        protected TCommand Command
        {
            get;
            private set;
        }
    }
}
