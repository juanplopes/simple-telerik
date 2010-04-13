// (c) Copyright 2002-2010 Telerik 
// This source is subject to the GNU General Public License, version 2
// See http://www.gnu.org/licenses/gpl-2.0.html. 
// All other rights reserved.

namespace Telerik.Web.Mvc.Infrastructure.Implementation
{
    using System.Globalization;
    using System.Linq;
    using System.Linq.Expressions;
    using Telerik.Web.Mvc.Infrastructure.Implementation.Expressions;
    /// <summary>
    /// Represents an <see cref="AggregateFunction"/> that uses aggregate extension 
    /// methods provided in <see cref="Enumerable"/> using <see cref="SourceField"/>
    /// as a member selector.
    /// </summary>
    public abstract class EnumerableSelectorAggregateFunction : EnumerableAggregateFunctionBase
    {
        /// <summary>
        /// Creates the aggregate expression using <see cref="EnumerableSelectorAggregateFunctionExpressionBuilder"/>.
        /// </summary>
        /// <param name="enumerableExpression">The grouping expression.</param>
        /// <returns></returns>
        public override Expression CreateAggregateExpression(Expression enumerableExpression)
        {
            var builder = new EnumerableSelectorAggregateFunctionExpressionBuilder(enumerableExpression, this);
            return builder.CreateAggregateExpression();
        }


        /// <summary>
        /// Gets or sets the name of the field, of the item from the set of items, which value is used as the argument of the aggregate function.
        /// </summary>
        /// <value>The name of the field to get the argument value from.</value>
        public virtual string SourceField
        {
            get;
            set;
        }

        /// <summary>
        /// Generates default name for this function using <see cref="EnumerableAggregateFunctionBase.AggregateMethodName"/> 
        /// and <see cref="SourceField"/>.
        /// </summary>
        /// <returns>
        /// Function name generated with the following pattern: 
        /// {<see cref="EnumerableAggregateFunctionBase.AggregateMethodName"/>}{<see cref="SourceField"/>}_{<see cref="object.GetHashCode"/>}
        /// </returns>
        protected override string GenerateFunctionName()
        {
            return string.Format(CultureInfo.InvariantCulture, "{0}{1}_{2}", this.AggregateMethodName, this.SourceField, this.GetHashCode());
        }
    }
}