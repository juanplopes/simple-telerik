// (c) Copyright 2002-2010 Telerik 
// This source is subject to the GNU General Public License, version 2
// See http://www.gnu.org/licenses/gpl-2.0.html. 
// All other rights reserved.

namespace Telerik.Web.Mvc.Infrastructure.Implementation
{
    using System;
    using System.Globalization;
    using System.Linq;

    /// <summary>
    /// Base class for all aggregate functions that will use extension 
    /// methods in <see cref="Enumerable"/> for aggregation.
    /// </summary>
    public abstract class EnumerableAggregateFunctionBase : AggregateFunction
    {
        /// <summary>
        /// Gets the name of the aggregate method on the <see cref="ExtensionMethodsType"/>
        /// that will be used for aggregation.
        /// </summary>
        /// <value>The name of the aggregate method that will be used.</value>
        protected internal abstract string AggregateMethodName { get; }

        /// <summary>
        /// Gets the type of the extension methods that holds the extension methods for
        /// aggregation. For example <see cref="Enumerable"/> or <see cref="Queryable"/>.
        /// </summary>
        /// <value>
        /// The type of that holds the extension methods. The default value is <see cref="Enumerable"/>.
        /// </value>
        protected virtual internal Type ExtensionMethodsType
        {
            get
            {
                return typeof(Enumerable);
            }
        }

        /// <summary>
        /// Generates default name for this function using <see cref="AggregateMethodName"/>.
        /// </summary>
        /// <returns>
        /// Function name generated with the following pattern: 
        /// {<see cref="AggregateMethodName"/>}_{<see cref="object.GetHashCode"/>}
        /// </returns>
        protected override string GenerateFunctionName()
        {
            return string.Format(CultureInfo.InvariantCulture, "{0}_{1}", this.AggregateMethodName, this.GetHashCode());
        }
    }
}