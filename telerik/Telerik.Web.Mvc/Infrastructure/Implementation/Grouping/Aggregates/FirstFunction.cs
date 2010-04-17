// (c) Copyright 2002-2010 Telerik 
// This source is subject to the GNU General Public License, version 2
// See http://www.gnu.org/licenses/gpl-2.0.html. 
// All other rights reserved.

namespace Telerik.Web.Mvc.Infrastructure.Implementation
{
    /// <summary>
	/// Represents a function that returns the first item from a set of items.
	/// </summary>
    public class FirstFunction : EnumerableAggregateFunction
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="FirstFunction"/> class.
		/// </summary>
		public FirstFunction()
		{
		}

        /// <summary>
        /// Gets the the First method name.
        /// </summary>
        /// <value><c>First</c>.</value>
        protected internal override string AggregateMethodName
        {
            get
            {
                return "FirstOrDefault";
            }
        }
	}
}
