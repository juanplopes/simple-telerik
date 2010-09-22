// (c) Copyright 2002-2010 Telerik 
// This source is subject to the GNU General Public License, version 2
// See http://www.gnu.org/licenses/gpl-2.0.html. 
// All other rights reserved.

namespace Telerik.Web.Mvc.UI
{
    using Extensions;
    using Infrastructure;
    
    public abstract class GridToolBarCommandBuilderBase<TModel, TCommand, TBuilder> : IHideObjectMembers
        where TModel : class
        where TCommand : GridToolBarCommandBase<TModel>
        where TBuilder : GridToolBarCommandBuilderBase<TModel, TCommand, TBuilder>
    {
        protected GridToolBarCommandBuilderBase(TCommand command)
        {
            Command = command;
        }

        //public TBuilder HtmlAttributes(object values)
        //{
        //    Command.HtmlAttributes = new RouteValueDictionary(values);

        //    return this as TBuilder;
        //}

        /// <summary>
        /// Sets the button type.
        /// </summary>
        /// <param name="type">The button type.</param>
        /// <returns></returns>
        public TBuilder ButtonType(GridButtonType type)
        {
            Guard.IsNotNull(type, "type");

            Command.ButtonType = type;

            return this as TBuilder;
        }

        /// <summary>
        /// Sets the HTML attributes.
        /// </summary>
        /// <param name="attributes">The HTML attributes.</param>
        /// <returns></returns>
        public TBuilder HtmlAttributes(object attributes)
        {
            Command.HtmlAttributes.Merge(attributes);

            return this as TBuilder;
        }

        /// <summary>
        /// Sets the image HTML attributes.
        /// </summary>
        /// <param name="attributes">The Image HTML attributes.</param>
        /// <returns></returns>
        public TBuilder ImageHtmlAttributes(object attributes)
        {
            Command.ImageHtmlAttributes.Merge(attributes);

            return this as TBuilder;
        }

        protected TCommand Command
        {
            get;
            private set;
        }
    }
}