// (c) Copyright 2002-2010 Telerik 
// This source is subject to the GNU General Public License, version 2
// See http://www.gnu.org/licenses/gpl-2.0.html. 
// All other rights reserved.
namespace Telerik.Web.Mvc.UI.Fluent
{
    using System;
    using System.Linq.Expressions;

    /// <summary>
    /// Creates data key for the <see cref="Grid{T}" />.
    /// </summary>
    /// <typeparam name="TModel">The type of the data item</typeparam>
    public class GridDataKeyFactory<TModel> : IHideObjectMembers
        where TModel : class
    {
        private readonly Grid<TModel> grid;

        /// <summary>
        /// Initializes a new instance of the <see cref="GridDataKeyFactory&lt;TModel&gt;"/> class.
        /// </summary>
        /// <param name="grid">The grid.</param>
        public GridDataKeyFactory(Grid<TModel> grid)
        {
            this.grid = grid;
        }

        /// <summary>
        /// Defines a data key.
        /// </summary>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="expression"></param>
        /// <returns></returns>
        public GridDataKeyBuilder<TModel, TValue> Add<TValue>(Expression<Func<TModel, TValue>> expression)
        {
            GridDataKey<TModel, TValue> dataKey = new GridDataKey<TModel, TValue>(expression);
            
            grid.DataKeys.Add(dataKey);

            return new GridDataKeyBuilder<TModel, TValue>(dataKey);
        }
    }
}
