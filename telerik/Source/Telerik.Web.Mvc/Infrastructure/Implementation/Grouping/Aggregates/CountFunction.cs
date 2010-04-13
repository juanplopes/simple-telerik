// (c) Copyright 2002-2010 Telerik 
// This source is subject to the GNU General Public License, version 2
// See http://www.gnu.org/licenses/gpl-2.0.html. 
// All other rights reserved.

namespace Telerik.Web.Mvc.Infrastructure.Implementation
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Globalization;

    /// <summary>
	/// Represents a function that returns the number of items in a set of items, including nested sets.
	/// </summary>
	public class CountFunction : EnumerableAggregateFunction
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="CountFunction"/> class.
		/// </summary>
		public CountFunction()
		{
		}

        /// <summary>
        /// Gets the the Count method name.
        /// </summary>
        /// <value><c>Count</c>.</value>
        protected internal override string AggregateMethodName
        {
            get
            {
                return "Count";
            }
        }
	}
}