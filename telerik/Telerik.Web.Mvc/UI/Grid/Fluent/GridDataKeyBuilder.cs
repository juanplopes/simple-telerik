// (c) Copyright 2002-2010 Telerik 
// This source is subject to the GNU General Public License, version 2
// See http://www.gnu.org/licenses/gpl-2.0.html. 
// All other rights reserved.
namespace Telerik.Web.Mvc.UI.Fluent
{
    using Infrastructure;

    /// <summary>
    /// Defines the fluent interface for configuring the data key.
    /// </summary>
    /// <typeparam name="TModel">The type of the model</typeparam>
    /// <typeparam name="TValue">The type of the value.</typeparam>
    public class GridDataKeyBuilder<TModel, TValue> : IHideObjectMembers
        where TModel : class
    {
        private readonly GridDataKey<TModel, TValue> dataKey;

        /// <summary>
        /// Initializes a new instance of the <see cref="GridDataKeyBuilder&lt;TModel, TValue&gt;"/> class.
        /// </summary>
        /// <param name="dataKey">The dataKey.</param>
        public GridDataKeyBuilder(GridDataKey<TModel, TValue> dataKey)
        {
            this.dataKey = dataKey;
        }

        /// <summary>
        /// Sets the RouteKey.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public void RouteKey(string value)
        {
            Guard.IsNotNullOrEmpty(value, "value");

            dataKey.RouteKey = value;
        }
    }
}
