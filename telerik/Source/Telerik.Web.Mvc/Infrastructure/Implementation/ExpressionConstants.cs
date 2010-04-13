// (c) Copyright 2002-2010 Telerik 
// This source is subject to the GNU General Public License, version 2
// See http://www.gnu.org/licenses/gpl-2.0.html. 
// All other rights reserved.

namespace Telerik.Web.Mvc.Infrastructure.Implementation
{
    using System.Linq.Expressions;
    
    internal class ExpressionConstants
    {
        internal static readonly Expression TrueLiteral = Expression.Constant(true);
        internal static readonly Expression FalseLiteral = Expression.Constant(false);
        internal static readonly Expression NullLiteral = Expression.Constant(null);
    }
}
